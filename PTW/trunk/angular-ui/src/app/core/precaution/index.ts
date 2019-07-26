// Context
export { ECommerceDataContext } from './_server/precaution.data-context';

// Models and Consts
export { PrecautionQAModel } from './_models/precaution.model';



export { SPECIFICATIONS_DICTIONARY } from './_consts/specification.dictionary';

// DataSources
export { CustomersDataSource } from './_data-sources/precaution.datasource';




// Actions
// Customer Actions =>
export {
    CustomerActionTypes,
    CustomerActions,
    CustomerOnServerCreated,
    CustomerCreated,
    CustomerUpdated,
    CustomersStatusUpdated,
    OneCustomerDeleted,
    ManyCustomersDeleted,
    CustomersPageRequested,
    CustomersPageLoaded,
    CustomersPageCancelled,
    CustomersPageToggleLoading
} from './_actions/precaution.actions';



// Effects
export { CustomerEffects } from './_effects/precaution.effects';


// Reducers
export { customersReducer } from './_reducers/precaution.reducers';


// Selectors
// Customer selectors =>
export {
    selectCustomerById,
    selectCustomersInStore,
    selectCustomersPageLoading,
    selectLastCreatedCustomerId,
    selectCustomersActionLoading,
    selectCustomersShowInitWaitingMessage
} from './_selectors/precaution.selectors';


// Services
export { CustomersService } from './_services/';

