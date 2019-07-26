
import { ConstructionToolListComponent } from './construction-tool-list.component';
import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

@NgModule({
  declarations: [ConstructionToolListComponent],
  imports: [
    CommonModule,
    RouterModule.forChild([
      {
        path:'',
        component: ConstructionToolListComponent
      },
    ]),
  ]
})
export class ConstructionToolListModule { }
