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
