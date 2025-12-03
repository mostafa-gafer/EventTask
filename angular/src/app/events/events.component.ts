import {
  FormGroup,
  FormBuilder,
  Validators,
  FormsModule,
  ReactiveFormsModule,
} from '@angular/forms';
import { Component, inject, OnInit } from '@angular/core';
import { DatePipe, formatDate } from '@angular/common';
import { NgbDatepickerModule, NgbDateStruct, NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
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

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  imports: [
    FormsModule,
    ReactiveFormsModule,
    NgbDatepickerModule,
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
  private fb = inject(FormBuilder);
  private confirmation = inject(ConfirmationService);

  event = { items: [], totalCount: 0 } as PagedResultDto<EventDto>;
  selectedEvent = {} as EventDto; // declare selectedEvent
  form: FormGroup;
  isModalOpen = false;
  organizers: any;

  ngOnInit() {
    const eventStreamCreator = query => this.eventService.getList(query);

    this.list.hookToQuery(eventStreamCreator).subscribe(response => {
      this.event = response;
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
    this.form = this.fb.group({
      nameEn: [this.selectedEvent.nameEn || '', Validators.required],
      nameAr: [this.selectedEvent.nameAr || '', Validators.required],
      startDate: [
        this.selectedEvent.startDate ? this.parseDate(this.selectedEvent.startDate) : null,
        Validators.required,
      ],
      endDate: [
        this.selectedEvent.endDate ? this.parseDate(this.selectedEvent.endDate) : null,
        Validators.required,
      ],
      organizerId: [this.selectedEvent.organizerId || '', Validators.required],
      link: [this.selectedEvent.link || null, Validators.required],
      location: [this.selectedEvent.location || null, Validators.required],
      isActive: [this.selectedEvent.isActive || false, Validators.required],
      isOnline: [this.selectedEvent.isOnline || false, Validators.required],
    });
  }

  save() {
    if (this.form.invalid) {
      return;
    }

    const formValue = this.form.value;
    const requestData = {
      ...formValue,
      startDate: this.formatDate(formValue.startDate),
      endDate: this.formatDate(formValue.endDate),
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

  private parseDate(value: string | Date): NgbDateStruct | null {
    if (!value) {
      return null;
    }

    const date = new Date(value);
    if (isNaN(date.getTime())) {
      return null;
    }

    return {
      year: date.getFullYear(),
      month: date.getMonth() + 1,
      day: date.getDate(),
    };
  }

  private formatDate(dateStruct: NgbDateStruct | null): string {
    if (!dateStruct) {
      return '';
    }

    const date = new Date(dateStruct.year, dateStruct.month - 1, dateStruct.day);
    return formatDate(date, 'yyyy-MM-dd', 'en');
  }
}
