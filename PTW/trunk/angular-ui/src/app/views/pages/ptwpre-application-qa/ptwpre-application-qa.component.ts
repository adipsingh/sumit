import { Component, OnInit } from "@angular/core";

export interface PtwQuestion {
	name: string;
	position: number;
	cold: string;
	hot: string;
	setting: string;
}

const ELEMENT_DATA: PtwQuestion[] = [
	{ position: 1, name: 'Does Your Work Generate Heat Or Sparks (Grinding , Cutting, Welding) ?', cold: 'No', hot: 'yes', setting: '' },
	{ position: 2, name: 'Does Your Work Requires Electrical Or Combustion Engine Equipment (Egnition source) Within a SIMPOS For Commissioning Area ?', cold: 'No', hot: 'yes', setting: '' },
];




@Component({
	selector: 'kt-ptwpre-application-qa',
	templateUrl: './ptwpre-application-qa.component.html',
	styleUrls: ['ptwpre-application-qa.component.scss'],
})

export class PTWPreApplicationQAComponent implements OnInit {
	columns: string[] = ['position', 'name', 'cold', 'hot', 'setting'];
	dataSource = ELEMENT_DATA;

	ngOnInit() {

	}
}