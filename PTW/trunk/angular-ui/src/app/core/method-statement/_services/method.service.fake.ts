// Angular
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
// RxJS
import { Observable } from 'rxjs';
// CRUD
import { HttpUtilsService, QueryParamsModel, QueryResultsModel } from '../../_base/crud';
import { CategoryModel } from '../_models/method.model';
import { environment } from '../../../../../src/environments/environment';
// Models 

const API_METHOD_STATEMENT_URL = 'api/methods';
@Injectable()
export class MethodService {
	constructor(private http: HttpClient, private httpUtils: HttpUtilsService) { }

	createCategory(data) {
		const httpHeader = this.httpUtils.getHTTPHeaders(); 
		return this.http.post(API_METHOD_STATEMENT_URL , data, { headers: httpHeader });

		// const httpOptions = {
		// 	headers: new HttpHeaders({
		// 		'Content-Type': 'application/json'
		// 	})
		// };
		// return this.http.post(API_CONSTRUCTION_CATEGORY_URL + '/Add', category, httpOptions);
	}

	getAllMethod(): Observable<CategoryModel[]> {
		
		return this.http.get<CategoryModel[]>(API_METHOD_STATEMENT_URL);
	}

	getCategoryById(categoryId: number): Observable<CategoryModel> {
		return this.http.get<CategoryModel>(API_METHOD_STATEMENT_URL + `/${categoryId}`);
	}

	findMethod(queryParams: QueryParamsModel): Observable<QueryResultsModel> {
		
		const httpHeaders = this.httpUtils.getHTTPHeaders();
		const httpParams = this.httpUtils.getFindHTTPParams(queryParams);
		const url = API_METHOD_STATEMENT_URL;
		return this.http.get<QueryResultsModel>(url, {
			headers: httpHeaders,
			params: httpParams
		});
	}

	updateCategory(Category) {
		const httpHeader = this.httpUtils.getHTTPHeaders(); 
		return this.http.post(API_METHOD_STATEMENT_URL + Category.ID, Category, { headers: httpHeader });
	}


	deleteCategory(categoryId: number) {
		const url = `${API_METHOD_STATEMENT_URL}/${categoryId}`;
		return this.http.post(url, {});
	}

}
