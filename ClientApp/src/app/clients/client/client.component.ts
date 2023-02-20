import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { RouterParamsService } from '../../router-params.service';

@Component({
  selector: 'app-client',
  templateUrl: './client.component.html',
  styleUrls: ['./client.component.scss']
})
export class ClientComponent implements OnInit {
  public customPatterns = { 'F': { pattern: new RegExp('\[a-zA-Zа-яА-Я\]'), optional: true} }
  public lookUps?: ILookupsModel;
  public client?: IClientModel;
  public isCreateMode: boolean = false;

  constructor(private route: ActivatedRoute,
              private http: HttpClient,
              private router: Router,
              private params: RouterParamsService,
              @Inject('BASE_URL') private baseUrl: string) { }

  async ngOnInit(): Promise<void> {
    this.isCreateMode = !this.params.clientId;
    this.lookUps = await firstValueFrom(this.http.get<ILookupsModel>(this.baseUrl + 'lookups'));
    this.refreshClient();
  }

  private async refreshClient(): Promise<void> {
    const clientId = this.params.clientId;
    if (!this.isCreateMode) {
      this.client = await firstValueFrom(this.http.get<IClientModel>(this.baseUrl + 'clients/' + clientId));
      console.log(this.client);
    } else {
      this.client = {
        birthDate: new Date(),
        birthPlace: "",
        citizenship: {
          id: 1,
          name: ""
        },
        disability: {
          id: 1,
          name: ""
        },
        email: "",
        familyState: {
          id: 1,
          name: ""
        },
        firstName: "",
        homePhone: "",
        id: 1,
        isMale: false,
        lastName: "",
        livingAddress: "",
        livingCity: {
          id: 1,
          name: ""
        },
        middleName: "",
        mobilePhone: "",
        monthIncome: undefined,
        passportId: "",
        passportIssuedAt: new Date(),
        passportIssuedBy: "",
        passportNumber: "",
        passportSeries: "",
        pensioner: false,
        placeOfWork: "",
        registrationCity: {
          id: 1,
          name: ""
        },
        workingPosition: ""

      }
    }
  }

  public async deleteClient(): Promise<void> {
    await firstValueFrom(this.http.post<void>(this.baseUrl + 'clients/' + this.params.clientId + '/delete', null));
    await this.router.navigateByUrl(`/clients`);
  }

  public async save(): Promise<void> {
    const id = await firstValueFrom(this.http.put<number>(this.baseUrl + 'clients/' + this.params.clientId, this.client))
      .catch(p => alert('Вы попытались сломать систему. Соответствующая информация будет отправлена вашему непосредственному начальнику'));
    if (this.isCreateMode) {
      await this.router.navigateByUrl(`/clients/` + id);
    } else {
      await this.refreshClient();
    }
    alert('Клиент успешно сохранён');
  }
}
