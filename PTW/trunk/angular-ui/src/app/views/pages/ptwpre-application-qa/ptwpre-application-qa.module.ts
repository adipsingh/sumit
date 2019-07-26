
import { RouterModule, Routes } from '@angular/router';
import { PTWPreApplicationQAComponent } from './ptwpre-application-qa.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  MatInputModule,
  
	MatListModule,
	MatPaginatorModule,
	MatProgressSpinnerModule,
	MatSortModule,
	MatTableModule,
	MatSelectModule,
	MatMenuModule,
	MatProgressBarModule,
	MatButtonModule,
	MatCheckboxModule,
	MatDialogModule,
	MatTabsModule,
	MatNativeDateModule,
	MatCardModule,
	MatRadioModule,
	MatIconModule,
	MatDatepickerModule,
	MatAutocompleteModule,
	MAT_DIALOG_DEFAULT_OPTIONS,
	MatSnackBarModule,
	MatTooltipModule,
  MAT_CHECKBOX_CLICK_ACTION
} from '@angular/material';
import { NgbProgressbarModule } from '@ng-bootstrap/ng-bootstrap';
import { environment } from '../../../../../src/environments/environment';
import { HttpClientInMemoryWebApiModule } from 'angular-in-memory-web-api';
import { FakeApiService } from '../../../../../src/app/core/_base/layout';
import { StoreModule } from '@ngrx/store';
import { ptwqaReducer, QuestionEffects, PTWQAService } from '../../../../../src/app/core/ptwqa';
import { EffectsModule } from '@ngrx/effects';
import { InterceptService, TypesUtilsService, LayoutUtilsService, HttpUtilsService } from '../../../../../src/app/core/_base/crud';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { ActionNotificationComponent, DeleteEntityDialogComponent, FetchEntityDialogComponent, UpdateStatusDialogComponent } from '../../partials/content/crud';
import { PartialsModule } from '../../partials/partials.module';
import { NgxPermissionsModule } from 'ngx-permissions';
import { EditComponent } from './edit/edit.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ListComponent } from './list/list.component';
import { AddPTWTypeDialogComponent } from './add-ptwtype/add-ptwtype.dialog.component';


const routes: Routes = [
	{
		path: '',
		component: PTWPreApplicationQAComponent,
		children: [
			{
				path: '',
				redirectTo: 'ptwpre-application-qa',
				pathMatch: 'full'
			},
			{
				path: 'ptwpre-application-qa',
				component: ListComponent
			},
			{
				path: 'add',
				component: EditComponent
			},
			{
				path: 'add/:id',
				component: EditComponent
			},
			{
				path: 'edit',
				component: EditComponent
			},
			{
				path: 'edit/:id',
				component: EditComponent
			},
		]
	}
];

@NgModule({
  declarations: [PTWPreApplicationQAComponent, EditComponent, ListComponent,AddPTWTypeDialogComponent],
  imports: [
      CommonModule,
      RouterModule.forChild(routes),
      HttpClientModule,
      MatInputModule,
      FormsModule,
		  ReactiveFormsModule,
      MatListModule,
      PartialsModule,
      NgxPermissionsModule.forChild(),
      MatPaginatorModule,
      MatProgressSpinnerModule,
      MatSortModule,
      MatTableModule,
      MatSelectModule,
      MatMenuModule,
      MatProgressBarModule,
      MatButtonModule,
      MatCheckboxModule,
      MatDialogModule,
      MatTabsModule,
      MatNativeDateModule,
      MatCardModule,
      MatRadioModule,
      MatIconModule,
      MatDatepickerModule,
      MatAutocompleteModule,
     // MAT_DIALOG_DEFAULT_OPTIONS,
      MatSnackBarModule,
      MatTooltipModule,
     //MAT_CHECKBOX_CLICK_ACTION,
     NgbProgressbarModule,
		environment.isMockEnabled ? HttpClientInMemoryWebApiModule.forFeature(FakeApiService, {
			passThruUnknownUrl: true,
        	dataEncapsulation: false
		}) : [],
		StoreModule.forFeature('questions', ptwqaReducer),
		EffectsModule.forFeature([QuestionEffects]),
    /*RouterModule.forChild([
      {
        path:'',
        component: PTWPreApplicationQAComponent
      },

    ]),*/
  ],
  providers: [    
    InterceptService,
      {
      provide: HTTP_INTERCEPTORS,
        useClass: InterceptService,
      multi: true
      },
    {
      provide: MAT_DIALOG_DEFAULT_OPTIONS,
      useValue: {
        hasBackdrop: true,
        panelClass: 'kt-mat-dialog-container__wrapper',
        height: 'auto',
        width: '900px'
      }
    },
    TypesUtilsService,
    LayoutUtilsService,
    HttpUtilsService,
    PTWQAService,	
    TypesUtilsService,
    LayoutUtilsService
  ],
  entryComponents: [
    ActionNotificationComponent,    
    DeleteEntityDialogComponent,
    FetchEntityDialogComponent,
	UpdateStatusDialogComponent,
	AddPTWTypeDialogComponent,
  ],
})
export class PTWPreApplicationQAModule { }