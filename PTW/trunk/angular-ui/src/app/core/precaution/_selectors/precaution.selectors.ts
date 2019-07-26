// NGRX
import { createFeatureSelector, createSelector } from '@ngrx/store';
// Lodash
import { each } from 'lodash';
// CRUD
import { QueryResultsModel, HttpExtenstionsModel } from '../../_base/crud';
// State
import { CustomersState } from '../_reducers/precaution.reducers';
import { PrecautionQAModel } from '../_models/precaution.model';

export const selectCustomersState = createFeatureSelector<CustomersState>('certificateQuestions');

export const selectCustomerById = (customerId: number) => createSelector(
    selectCustomersState,
    customersState => customersState.entities[customerId]
);

export const selectCustomersPageLoading = createSelector(
    selectCustomersState,
    customersState => customersState.listLoading
);

export const selectCustomersActionLoading = createSelector(
    selectCustomersState,
    customersState => customersState.actionsloading
);

export const selectLastCreatedCustomerId = createSelector(
    selectCustomersState,
    customersState => customersState.lastCreatedCustomerId
);

export const selectCustomersShowInitWaitingMessage = createSelector(
    selectCustomersState,
    customersState => customersState.showInitWaitingMessage
);

export const selectCustomersInStore = createSelector(
    selectCustomersState,
    customersState => {
        const items: PrecautionQAModel[] = [];
        each(customersState.entities, element => {
            items.push(element);
        });
        const httpExtension = new HttpExtenstionsModel();
        const result: PrecautionQAModel[] = httpExtension.sortArray(items, customersState.lastQuery.sortField, customersState.lastQuery.sortOrder);
        return new QueryResultsModel(result, customersState.totalCount, '');
    }
);
