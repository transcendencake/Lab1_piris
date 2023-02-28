import {Component, Inject, OnInit} from '@angular/core';
import {firstValueFrom} from "rxjs";
import {ActivatedRoute, Router} from "@angular/router";
import {HttpClient} from "@angular/common/http";
import {RouterParamsService} from "../router-params.service";

enum ActionType {
  AuthAccount = 1,
  AuthPin = 2,
  SelectActions = 3,
  Balance = 4,
  MenuOrExit = 5,
  Withdraw = 6,
  Blocked = 7
}

@Component({
  selector: 'app-atm',
  templateUrl: './atm.component.html',
  styleUrls: ['./atm.component.css']
})
export class AtmComponent implements OnInit {
  public actionTypes = ActionType;
  public currActionType = ActionType.AuthAccount;
  public balance = 0;
  public accountNumber = '';
  public creditId = 0;
  public withdrawAmount = 0;
  public pin = '';
  public invalidAttemptsCount = 0;

  constructor(private route: ActivatedRoute,
              private http: HttpClient,
              private router: Router,
              private params: RouterParamsService,
              @Inject('BASE_URL') private baseUrl: string) {
  }

  ngOnInit(): void {
  }

  public async pinEntered(): Promise<void> {
    try {
      this.creditId = await firstValueFrom(this.http.post<number>(this.baseUrl + 'atm/pin',
        {
          accountNumber: this.accountNumber,
          pin: this.pin
        }));

      this.invalidAttemptsCount = 0;
      this.currActionType = ActionType.SelectActions;
    }
    catch (e) {
      this.invalidAttemptsCount++;
      alert('Неверный номер карты или пин');
      if (this.invalidAttemptsCount >= 3) {
        this.currActionType = ActionType.Blocked;
      } else {
        this.currActionType = ActionType.AuthAccount;
      }
    }
  }

  public accountEntered(): void {
    this.currActionType = ActionType.AuthPin;
  }

  public menuSelected(): void {
    this.currActionType = ActionType.SelectActions;
  }

  public exitSelected(): void {
    this.currActionType = ActionType.AuthAccount;
    this.creditId = 0;
    this.accountNumber = '';
    this.pin = '';
  }

  public async balanceSelected(): Promise<void> {
    this.balance = await firstValueFrom(this.http.get<number>(this.baseUrl + 'atm/' + this.creditId));
    this.currActionType = ActionType.Balance;
    if (confirm('Печатать чек?')) {
      alert(`Ваш баланс - ${this.balance}`);
    }
  }

  public async withdrawAmountEntered(): Promise<void> {
    try {
      await firstValueFrom(this.http.post<void>(this.baseUrl + 'atm/' + this.creditId, {amount: this.withdrawAmount}));
      if (confirm('Печатать чек?')) {
        alert(`Вы сняли с кредитного счёта ${this.withdrawAmount}`);
      }
    }
    catch {
      alert(`На балансе недостаточно денег`);
    }
    if (confirm('Желаете продолжить?')) {
      this.currActionType = ActionType.SelectActions;
    } else {
      this.exitSelected();
    }
  }

  public withdrawSelected(): void {
    this.currActionType = ActionType.Withdraw;
  }
}
