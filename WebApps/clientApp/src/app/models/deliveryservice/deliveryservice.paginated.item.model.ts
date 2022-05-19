import { Injectable } from '@angular/core';
import { PaginatedItemsModel } from '../common/paginated.items.model';
import { DeliveryServiceModel } from './deliveryservice.model';


@Injectable()
export class DeliveryServicePaginatedItemModel extends PaginatedItemsModel{
    data:DeliveryServiceModel[];
}