
import { PCWBSDefinationComponent } from './pcwbsdefination.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
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



@NgModule({
  declarations: [PCWBSDefinationComponent],
  imports: [
    CommonModule,
    MatInputModule,
	  MatPaginatorModule,
  MatProgressSpinnerModule,
	MatSortModule,
	MatTableModule,
	MatListModule,
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
	MatSnackBarModule,
	MatTooltipModule,
    RouterModule.forChild([
			{
				path: '',
				component: PCWBSDefinationComponent
			},
		]),
  ],
  providers:[
          {
        provide: MAT_DIALOG_DEFAULT_OPTIONS,
        useValue: {
          hasBackdrop: true,
          panelClass: 'kt-mat-dialog-container__wrapper',
          height: 'auto',
          width: '900px'
        }
        
      },
      {
        provide:MAT_CHECKBOX_CLICK_ACTION, useValue: 'check'
      }
       
  ]
})
export class PCWBSDefinationModule { }
