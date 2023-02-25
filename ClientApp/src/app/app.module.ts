import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { MAT_FORM_FIELD_DEFAULT_OPTIONS } from '@angular/material/form-field';
import {MatFormFieldModule} from '@angular/material/form-field';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { ClientsComponent } from './clients/clients/clients.component';
import { ClientComponent } from './clients/client/client.component';
import { MatInputModule } from '@angular/material/input';
import { NgxMaskModule } from 'ngx-mask';
import { MatSelectModule } from '@angular/material/select';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';
import { MatToolbarModule } from '@angular/material/toolbar';
import { FormsModule } from '@angular/forms';
import {MatRadioModule} from "@angular/material/radio";
import {MatCheckboxModule} from "@angular/material/checkbox";
import { ClientGeneralComponent } from './clients/client-general/client-general.component';
import {MatTabsModule} from "@angular/material/tabs";
import { DepositsComponent } from './clients/client-general/deposits/deposits.component';
import { DepositComponent } from './clients/client-general/deposits/deposit/deposit.component';
import { AccountsComponent } from './clients/client-general/accounts/accounts.component';
import { CreditsComponent } from './clients/client-general/credits/credits.component';
import { CreditComponent } from './clients/client-general/credits/credit/credit.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    ClientsComponent,
    ClientComponent,
    ClientGeneralComponent,
    DepositsComponent,
    DepositComponent,
    AccountsComponent,
    CreditsComponent,
    CreditComponent
  ],
  imports: [
    BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
    BrowserAnimationsModule,
    HttpClientModule,
    RouterModule.forRoot([
      {path: '', component: HomeComponent, pathMatch: 'full'},
      {path: 'counter', component: CounterComponent},
      {path: 'fetch-data', component: FetchDataComponent},
      {
        path: 'clients',
        children: [
          {path: '', pathMatch: 'full', component: ClientsComponent},
          {
            path: ':clientId',
            component: ClientGeneralComponent,
            children: [
              {
                path: 'general',
                component: ClientComponent
              },
              {
                path: 'accounts',
                component: AccountsComponent
              },
              {
                path: 'deposits',
                children: [
                  {
                    path: '',
                    pathMatch: 'full',
                    component: DepositsComponent
                  },
                  {
                    path: ':depositId',
                    component: DepositComponent
                  }
                ]
              },
              {
                path: 'credits',
                children: [
                  {
                    path: '',
                    pathMatch: 'full',
                    component: CreditsComponent
                  },
                  {
                    path: ':creditId',
                    component: CreditComponent
                  }
                ]
              },
              { path: '', pathMatch: 'full', redirectTo: 'general' },
            ]
          }
        ]
      },
    ]),
    FormsModule,
    CommonModule,
    MatSelectModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    NgxMaskModule.forRoot(),
    MatInputModule,
    MatToolbarModule,
    MatRadioModule,
    MatCheckboxModule,
    MatTabsModule
  ],
  providers: [{
      provide: MAT_FORM_FIELD_DEFAULT_OPTIONS,
      useValue: {
        appearance: 'fill'
      }
    }],
  bootstrap: [AppComponent]
})
export class AppModule { }
