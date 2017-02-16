import {Command,Query,PagedResult} from "./Common";
//Common
export class ValidationMessage{constructor(public MessageType: MessageType,public Key: string,public Message: string){}}
export class PaginationQuery{constructor(public PageNumber: number,public ItemsPerPage: number){}}
export enum MessageType{Info=1,Warning=2,Error=3,Exception=4}
//Infrastructure
export class GetVersionQuery extends Query<VersionModel>{constructor(){super('Infrastructure/GetVersion')}}
export class VersionModel{public Version: string;}
export class TestCommand extends Command<TestResult>{constructor(){super('Infrastructure/Test')}}
export class TestResult{public Message: string;}
//ProductCatalog
export class GetCartViewModelQuery extends Query<CartViewModel>{constructor(public UserId: string,public typescriptPlace: boolean){super('ProductCatalog/GetCartViewModel')}}
export class CartViewModel{public Items: CartItemViewModel[];public SubTotal: number;public RootEntityKey: string;public Id: string;}
export class CartItemViewModel{public Description: string;public Name: string;public Price: number;public RootEntityKey: string;public Id: string;}
export class GetHomepageCataglogViewModelQuery extends Query<HomePageViewModel>{constructor(){super('ProductCatalog/GetHomepageCataglogViewModel')}}
export class HomePageViewModel{public Products: HomePageProductViewModel[];public RootEntityKey: string;public Id: string;}
export class HomePageProductViewModel{public Name: string;public Price: number;public ShortDescription: string;public RootEntityKey: string;public Id: string;}
export class AddProductToCartCommand extends Command<Nop>{constructor(public UserId: string,public ProductId: string,public Quantity: number){super('ProductCatalog/AddProductToCart')}}
export enum Nop{NoValue=0}
export class CreateSampleCatalogCommand extends Command<Nop>{constructor(){super('ProductCatalog/CreateSampleCatalog')}}
//Users
//Security
export class GetNewSessionViewModelQuery extends Query<SessionViewModel>{constructor(public SessionId: string,public typescriptPlace: boolean){super('Security/GetNewSessionViewModel')}}
export class SessionViewModel{public Name: string;public RootEntityKey: string;public Id: string;}
export class CreateNewSessionViewModelCommand extends Command<Nop>{constructor(public SessionId: string,public typescriptPlace: boolean){super('Security/CreateNewSessionViewModel')}}
//Lead
export class GetLeadIndicatorOveriewQuery extends Query<LeadIndicatorViewModel>{constructor(public LeadId: string,public typescriptPlace: boolean){super('Lead/GetLeadIndicatorOveriew')}}
export class LeadIndicatorViewModel{public AllIndicators: IndicatorType[];public DataSources: IndicatorSource[];public Name: string;public Address: string;public Indicators: PerformanceIndicatorViewModel[];public BusinessMeasure: BusinesssMeasureViewModel;public RootEntityKey: string;public Id: string;}
export class BusinesssMeasureViewModel{public AnnualRevenue: number;public NumberOfEmployees: number;public EntytyType: EntityTypeValue;}
export enum EntityTypeValue{SoleProp=1,LLC=2}
export class PerformanceIndicatorViewModel{public IndicatorType: IndicatorType;public SelectedValue: IndicatorValueViewModel;public AllValues: IndicatorValueViewModel[];}
export class IndicatorValueViewModel{public LeadId: string;public Value: any;public Type: IndicatorType;public Source: IndicatorSource;}
export class IndicatorSource{public Value: number;public DisplayName: string;}
export class IndicatorType{public ValueType: any;public Value: number;public DisplayName: string;}
