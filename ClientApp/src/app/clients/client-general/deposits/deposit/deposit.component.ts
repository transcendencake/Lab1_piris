import {Component, Inject, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {HttpClient} from "@angular/common/http";
import {RouterParamsService} from "../../../../router-params.service";
import {firstValueFrom} from "rxjs";
import * as moment from "moment";

@Component({
  selector: 'app-deposit',
  templateUrl: './deposit.component.html',
  styleUrls: ['./deposit.component.css']
})
export class DepositComponent implements OnInit {
  public lookUps?: IDepositLookupsModel;
  public deposit?: IDepositModel;
  public isCreateMode: boolean = false;
  public timeSpan = 3;

  constructor(private route: ActivatedRoute,
              private http: HttpClient,
              private router: Router,
              private params: RouterParamsService,
              @Inject('BASE_URL') private baseUrl: string) { }

  async ngOnInit(): Promise<void> {
    this.isCreateMode = !this.params.depositId;
    this.lookUps = await firstValueFrom(this.http.get<IDepositLookupsModel>(this.baseUrl + 'lookups/deposit'));
    this.refreshDeposit();
  }

  private async refreshDeposit(): Promise<void> {
    const { clientId, depositId } = this.params;
    if (!this.isCreateMode) {
      this.deposit = await firstValueFrom(this.http.get<IDepositModel>(this.baseUrl + 'clients/' + clientId + '/deposit/' + depositId));
      console.log(this.deposit);
    } else {
      this.deposit = {
        contractNumber: "",
        currencyType: {
          id: 1,
          name: ""
        },
        amount: 0,
        startDate: new Date(),
        depositType: undefined,
        endDate: moment().add(this.timeSpan, 'months').toDate(),

        nextInterestPayDate: undefined,
        id: 0,
        isActive: true,
        interestAccount: undefined,
        depositAccount: undefined
      }
    }
  }

  public async save(): Promise<void> {
    const id = await firstValueFrom(this.http.post<number>(this.baseUrl + 'clients/' + this.params.clientId + '/deposit', this.deposit))
      .catch(p => alert('Вы попытались сломать систему. Соответствующая информация будет отправлена вашему непосредственному начальнику'));
    await this.router.navigateByUrl(`/clients/` + this.params.clientId + '/deposits/' + id);
    alert('Депозит успешно сохранён');
    await this.ngOnInit();
  }

  public updateDates(): void {
    // @ts-ignore
    this.deposit.endDate = moment().add(this.timeSpan, 'months').toDate();
  }

  public async payInterest(): Promise<void> {
    await firstValueFrom(this.http.post<number>(this.baseUrl + 'clients/' + this.params.clientId + '/deposit/' + this.params.depositId + '/pay-interest', null))
    alert('Готово!');
  }

  public async close(): Promise<void> {
    await firstValueFrom(this.http.post<number>(this.baseUrl + 'clients/' + this.params.clientId + '/deposit/' + this.params.depositId + '/close', null))
    alert('Депозит закрыт.');
    await this.ngOnInit();
  }
}
