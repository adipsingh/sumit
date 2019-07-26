import { Component, OnInit, Inject, ChangeDetectionStrategy, OnDestroy } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { Subscription } from 'rxjs';
import { Store } from '@ngrx/store';
import { AppState } from '../../../../core/reducers';
@Component({
	selector: 'kt-add-ptwtype-dialog',
	templateUrl: './add-ptwtype.dialog.component.html',
	changeDetection: ChangeDetectionStrategy.Default,
})
export class AddPTWTypeDialogComponent implements OnInit, OnDestroy {
	// Private properties
	private componentSubscriptions: Subscription;
    /**
     * Component constructor
     *
     * @param dialogRef: MatDialogRef<RoleEditDialogComponent>
     * @param data: any
     * @param store: Store<AppState>
     */
	constructor(public dialogRef: MatDialogRef<AddPTWTypeDialogComponent>,
		@Inject(MAT_DIALOG_DATA)
		public data: any, private store: Store<AppState>) { }
	ngOnInit() {
	}
    /**
     * On destroy
     */
	ngOnDestroy() {
		if (this.componentSubscriptions) {
			this.componentSubscriptions.unsubscribe();
		}
	}
}
