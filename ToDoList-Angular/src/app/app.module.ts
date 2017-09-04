import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule }   from '@angular/forms';
import { HttpModule }    from '@angular/http';
import { RouterModule, Routes } from  '@angular/router';

import { InMemoryWebApiModule} from 'angular-in-memory-web-api';
import { InMemoryDataService }  from './in-memory-data.service';

import { AppComponent }  from './app.component';
import { TasksComponent }  from './tasks.component';

import { TaskService } from './task.service';

const routers = [
    {
        path: '',
        component: TasksComponent,
        pathMatch: 'full'
    },
    {
        path: 'api/tasks',
        component: TasksComponent,
    }
];

@NgModule({
  imports:      [ BrowserModule,
                  FormsModule,
                  HttpModule,
                  RouterModule.forRoot(routers),
                  InMemoryWebApiModule.forRoot(InMemoryDataService) ],
  declarations: [ AppComponent,
                  TasksComponent ],
  bootstrap:    [ AppComponent ],
  providers:    [ TaskService,
                  InMemoryDataService,
      ]
})
export class AppModule { }
