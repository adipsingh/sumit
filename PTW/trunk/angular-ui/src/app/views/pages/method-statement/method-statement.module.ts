
import { RouterModule } from '@angular/router';
import { MethodStatementComponent } from './method-statement.component';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

@NgModule({
  declarations: [MethodStatementComponent],
  imports: [
    CommonModule,
    RouterModule.forChild([
      {
        path:'',
        component:MethodStatementComponent
      },
    ]),
  ]
})
export class MethodStatementModule { }
