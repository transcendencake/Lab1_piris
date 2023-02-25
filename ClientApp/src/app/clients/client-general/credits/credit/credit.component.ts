import {Component, Inject, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {HttpClient} from "@angular/common/http";
import {RouterParamsService} from "../../../../router-params.service";
import {firstValueFrom} from "rxjs";
import * as moment from "moment/moment";

@Component({
  selector: 'app-credit',
  templateUrl: './credit.component.html',
  styleUrls: ['./credit.component.css']
})
export class CreditComponent implements OnInit {
  public lookUps?: ICreditLookupsModel;
  public credit?: ICreditModel;
  public isCreateMode: boolean = false;
  constructor(private route: ActivatedRoute,
              private http: HttpClient,
              private router: Router,
              private params: RouterParamsService,
              @Inject('BASE_URL') private baseUrl: string) { }

  async ngOnInit(): Promise<void> {
    this.isCreateMode = !this.params.creditId;
    this.lookUps = await firstValueFrom(this.http.get<ICreditLookupsModel>(this.baseUrl + 'lookups/credit'));
    this.refreshCredit();
  }

  private async refreshCredit(): Promise<void> {
    const { clientId, creditId } = this.params;
    if (!this.isCreateMode) {
      this.credit = await firstValueFrom(this.http.get<ICreditModel>(this.baseUrl + 'clients/' + clientId + '/credit/' + creditId));
      console.log(this.credit);
    } else {
      this.credit = {
        contractNumber: "",
        currencyType: {
          id: 1,
          name: ""
        },
        amount: 0,
        startDate: new Date(),
        creditType: undefined,
        lengthInMonths: 3,
        endDate: moment().add(3, 'months').toDate(),

        nextInterestPayDate: undefined,
        id: 0,
        isActive: true,
        interestAccount: undefined,
        depositAccount: undefined
      }
    }
  }

  public async save(): Promise<void> {
    const id = await firstValueFrom(this.http.post<number>(this.baseUrl + 'clients/' + this.params.clientId + '/credit', this.credit))
      .catch(p => alert('Вы попытались сломать систему. Соответствующая информация будет отправлена вашему непосредственному начальнику'));
    await this.router.navigateByUrl(`/clients/` + this.params.clientId + '/credits/' + id);
    alert('Кредит успешно сохранён');
    await this.ngOnInit();
  }

  public updateDates(): void {
    // @ts-ignore
    this.credit.endDate = moment().add(this.credit?.lengthInMonths, 'months').toDate();
  }

  public async payInterest(): Promise<void> {
    await firstValueFrom(this.http.post<number>(this.baseUrl + 'clients/' + this.params.clientId + '/credit/' + this.params.creditId + '/pay-interest', null))
    alert('Готово!');
  }

  public async close(): Promise<void> {
    await firstValueFrom(this.http.post<number>(this.baseUrl + 'clients/' + this.params.clientId + '/credit/' + this.params.creditId + '/close', null))
    alert('Кредит закрыт.');
    await this.ngOnInit();
  }

  public calculateSchedule(): IScheduleItem[] {
    let monthPayment;
    if (this.credit?.creditType?.isDifferentiated) {
      monthPayment = this.credit.creditType.percent * this.credit.amount / 100;
    } else {
      // @ts-ignore
      let i = this.credit.creditType.percent / 12 / 100;
      // @ts-ignore
      let k = (i * Math.pow((1 + i), this.credit.lengthInMonths)) / (Math.pow((1 + i), this.credit.lengthInMonths) - 1);
      // @ts-ignore
      monthPayment = k * this.credit.amount;
    }
    const result = [];
    // @ts-ignore
    for (let i = 0; i < this.credit.lengthInMonths; i++) {
      result.push({
        amount: monthPayment,
        date: moment().add(i + 1, 'months').toDate()
      });
    }
    if (this.credit?.creditType?.isDifferentiated) {
      result[this.credit.lengthInMonths - 1].amount += this.credit.amount;
    }
    return result;
  }
}
