export interface OrderData {
    id: string;
    userId: string;
    requestedAmount?: number;
    totalFoundAmount: number;
    crawlType: CrawlType;
    products: ProductData[];
    createdOn: Date;
    orderEvents: OrderEventDto[];
}

export interface ProductData {
    id: string;
    name: string;
    picture: string;
    isOnSale: boolean;
    price: number;
    salePrice: string;
    createdOn: Date;
}

export interface OrderEventDto {
    orderId: string;
    status: OrderStatus;
    createdOn: Date;
}

export interface OrderRequestData {
    crawlType: CrawlType;
    isAmountEntered: boolean;
    requestedAmount: number;
    isDownloadChecked: boolean;
    isEmailChecked: boolean;
    email: string;
}

export enum CrawlType {
    All = 0,
    OnSale = 1,
    NormalPrice = 2,
}

export enum OrderStatus {
    BotStarted = 1,
    CrawlingStarted = 2,
    CrawlingCompleted = 3,
    CrawlingFailed = 4,
    OrderCompleted = 5,
}
