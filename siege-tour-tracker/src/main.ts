import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { environment } from './environments/environment';

if (environment.production) {
  enableProdMode();
}

platformBrowserDynamic()
    //start with the app-module controller
    .bootstrapModule(AppModule)
    //catch any errors that occur in our application and display them in the console
    .catch(err => console.error(err));
