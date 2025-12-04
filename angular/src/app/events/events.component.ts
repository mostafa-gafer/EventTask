import {
  FormGroup,
  FormBuilder,
  Validators,
  FormsModule,
  ReactiveFormsModule,
} from '@angular/forms';
import { Component, inject, OnInit } from '@angular/core';
import { DatePipe, formatDate } from '@angular/common';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import {
  ListService,
  PagedResultDto,
  LocalizationPipe,
  PermissionDirective,
  AutofocusDirective,
} from '@abp/ng.core';
import {
  ConfirmationService,
  Confirmation,
  NgxDatatableDefaultDirective,
  NgxDatatableListDirective,
  ModalCloseDirective,
  ModalComponent,
} from '@abp/ng.theme.shared';
import { EventService } from '../proxy/events';
import { EventDto } from '../proxy/events/dtos';
import { UserService } from '../proxy/users';

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  imports: [
    FormsModule,
    ReactiveFormsModule,
    NgxDatatableModule,
    NgbDropdownModule,
    ModalComponent,
    AutofocusDirective,
    NgxDatatableListDirective,
    NgxDatatableDefaultDirective,
    PermissionDirective,
    ModalCloseDirective,
    LocalizationPipe,
    DatePipe,
  ],
  providers: [ListService],
})
export class EventsComponent implements OnInit {
  public readonly list = inject(ListService);
  private eventService = inject(EventService);
  private userService = inject(UserService);
  private fb = inject(FormBuilder);
  private confirmation = inject(ConfirmationService);

  event = { items: [], totalCount: 0 } as PagedResultDto<EventDto>;
  selectedEvent = {} as EventDto; // declare selectedEvent
  form: FormGroup;
  isModalOpen = false;
  organizers: any;

  get isOnline(): boolean {
    return this.form?.get('isOnline')?.value || false;
  }

  ngOnInit() {
    const eventStreamCreator = query => this.eventService.getList(query);

    this.list.hookToQuery(eventStreamCreator).subscribe(response => {
      this.event = response;
    });

    this.userService.getAll().subscribe(response => {
      this.organizers = response;
    });
  }

  createEvent() {
    this.selectedEvent = {} as EventDto;
    this.buildForm();
    this.isModalOpen = true;
  }

  editEvent(id: string) {
    this.eventService.get(id).subscribe(event => {
      this.selectedEvent = event;
      this.buildForm();
      this.isModalOpen = true;
    });
  }

  delete(id: string) {
    this.confirmation.warn('::AreYouSureToDelete', '::AreYouSure').subscribe(status => {
      if (status === Confirmation.Status.confirm) {
        this.eventService.delete(id).subscribe(() => this.list.get());
      }
    });
  }

  buildForm() {
    const isOnline = this.selectedEvent.isOnline || false;

    this.form = this.fb.group({
      nameEn: [this.selectedEvent.nameEn || '', Validators.required],
      nameAr: [this.selectedEvent.nameAr || '', Validators.required],
      startDate: [
        this.selectedEvent.startDate ? this.parseDateTime(this.selectedEvent.startDate) : null,
        Validators.required,
      ],
      endDate: [
        this.selectedEvent.endDate ? this.parseDateTime(this.selectedEvent.endDate) : null,
        Validators.required,
      ],
      organizerId: [this.selectedEvent.organizerId || '', Validators.required],
      link: [this.selectedEvent.link || null, isOnline ? Validators.required : null],
      location: [this.selectedEvent.location || null, !isOnline ? Validators.required : null],
      capacity: [this.selectedEvent.capacity || null, !isOnline ? Validators.required : null],
      isActive: [this.selectedEvent.isActive || false, Validators.required],
      isOnline: [isOnline, Validators.required],
    });

    // Subscribe to isOnline changes to update validators dynamically
    this.form.get('isOnline')?.valueChanges.subscribe(value => {
      this.updateConditionalValidators(value);
    });
  }

  private updateConditionalValidators(isOnline: boolean) {
    const linkControl = this.form.get('link');
    const locationControl = this.form.get('location');
    const capacityControl = this.form.get('capacity');

    if (isOnline) {
      // If online: link is required, capacity and location are not required
      linkControl?.setValidators(Validators.required);
      locationControl?.clearValidators();
      capacityControl?.clearValidators();
    } else {
      // If not online: capacity and location are required, link is not required
      linkControl?.clearValidators();
      locationControl?.setValidators(Validators.required);
      capacityControl?.setValidators(Validators.required);
    }

    linkControl?.updateValueAndValidity();
    locationControl?.updateValueAndValidity();
    capacityControl?.updateValueAndValidity();
  }

  save() {
    if (this.form.invalid) {
      return;
    }

    const formValue = this.form.value;
    const requestData = {
      ...formValue,
      startDate: this.formatDateTime(formValue.startDate),
      endDate: this.formatDateTime(formValue.endDate),
    };

    let request = this.eventService.create(requestData);
    if (this.selectedEvent.id) {
      request = this.eventService.update(this.selectedEvent.id, requestData);
    }

    request.subscribe(() => {
      this.isModalOpen = false;
      this.form.reset();
      this.list.get();
    });
  }

  private parseDateTime(value: string | Date): string | null {
    if (!value) {
      return null;
    }

    const date = new Date(value);
    if (isNaN(date.getTime())) {
      return null;
    }

    // Format as datetime-local string (YYYY-MM-DDTHH:mm)
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const day = String(date.getDate()).padStart(2, '0');
    const hours = String(date.getHours()).padStart(2, '0');
    const minutes = String(date.getMinutes()).padStart(2, '0');

    return `${year}-${month}-${day}T${hours}:${minutes}`;
  }

  private formatDateTime(dateTimeString: string | null): string {
    if (!dateTimeString) {
      return '';
    }

    // Parse datetime-local string (YYYY-MM-DDTHH:mm) and format as ISO string
    const date = new Date(dateTimeString);
    if (isNaN(date.getTime())) {
      return '';
    }

    return date.toISOString();
  }
}
