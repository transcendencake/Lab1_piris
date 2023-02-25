interface ISelectedItemModel {
  id: number;
  name: string;
}

interface ILookupsModel {
  cities: ISelectedItemModel[];
  disabilities: ISelectedItemModel[];
  familyStates: ISelectedItemModel[];
  citizenships: ISelectedItemModel[];
}

interface IDepositLookupsModel {
  currencies: ISelectedItemModel[];
  depositTypes: IDepositTypeModel[];
}

interface ICreditLookupsModel {
  currencies: ISelectedItemModel[];
  creditTypes: ICreditTypeModel[];
}

interface IClientModel {
  id: number;
  firstName: string;
  lastName: string;
  middleName: string;
  birthDate: Date;
  isMale: boolean;
  passportSeries: string;
  passportNumber: string;
  passportIssuedBy: string;
  passportIssuedAt: Date;
  passportId: string;
  birthPlace: string;
  livingCity: ISelectedItemModel;
  livingAddress: string;
  homePhone: string;
  mobilePhone: string;
  email: string;
  placeOfWork: string;
  workingPosition: string;
  registrationCity: ISelectedItemModel;
  familyState: ISelectedItemModel;
  citizenship: ISelectedItemModel;
  disability: ISelectedItemModel;
  pensioner: boolean;
  monthIncome?: number;
}

interface IAccountModel {
  number: string;
  balance: number;
  isOpen: boolean;
  code: string;
  name: string;
  isActive: boolean;
}

interface IDepositModel {
  id: number;
  contractNumber: string;
  depositType?: IDepositTypeModel;
  currencyType: ISelectedItemModel;
  isActive: boolean;
  amount: number;
  startDate: Date;
  endDate: Date;
  nextInterestPayDate?: Date;
  depositAccount?: ISelectedItemModel;
  interestAccount?: ISelectedItemModel;
}

interface IDepositTypeModel extends ISelectedItemModel {
  percent: number;
  isRecallable: boolean;
}

interface ICreditModel {
  id: number;
  contractNumber: string;
  creditType?: ICreditTypeModel;
  currencyType: ISelectedItemModel;
  isActive: boolean;
  amount: number;
  startDate: Date;
  endDate: Date;
  lengthInMonths: number;
  nextInterestPayDate?: Date;
  depositAccount?: ISelectedItemModel;
  interestAccount?: ISelectedItemModel;
}

interface ICreditTypeModel extends ISelectedItemModel {
  percent: number;
  isDifferentiated: boolean;
}

interface IScheduleItem {
  amount: number;
  date: Date;
}
