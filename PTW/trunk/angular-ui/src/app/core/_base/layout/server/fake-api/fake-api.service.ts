// Angular
import { Injectable } from '@angular/core';
// Angular in memory
import { InMemoryDbService } from 'angular-in-memory-web-api';
// RxJS
import { Observable } from 'rxjs';
// Auth
import { AuthDataContext } from '../../../../auth';
// ECommerce
import { ECommerceDataContext } from '../../../../certificateqa';
import{CompanyDataContext}from '../../../../e-commerce'
// Models
import { CarsDb } from './fake-db/cars';
import{CertificateQADataContext}from '../../../../certificate-qa'
import{CompanyDetailsContext}from '../../../../company';



@Injectable()
export class  FakeApiService implements InMemoryDbService {
	/**
	 * Service Constructore
	 */
	constructor() {}

	/**
	 * Create Fake DB and API
	 */
	createDb(): {} | Observable<{}> {
		// tslint:disable-next-line:class-name
		const db = {
			// auth module
			users: AuthDataContext.users,
			roles: AuthDataContext.roles,
			permissions: AuthDataContext.permissions,

			// e-commerce
			// customers
			certificateQuestions: ECommerceDataContext.questions,
			// products
			company:CompanyDataContext.companies,
			section: CompanyDataContext.sections,
			productRemarks: CompanyDataContext.remarks,
			productSpecs: CompanyDataContext.carSpecs,

			// orders
			orders: CompanyDataContext.orders,
			companys:CompanyDetailsContext.companys,

			// data-table
			//questions:QuestionsDb.questions
		};
		return db;
	}
}
