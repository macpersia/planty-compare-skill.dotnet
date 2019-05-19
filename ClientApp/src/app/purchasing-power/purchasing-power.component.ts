import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-purchasing-power',
  templateUrl: './purchasing-power.component.html'
})
export class PurchasingPowerComponent {
  public records: PurchasingPower[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {

    // TODO: parameterize the base url
    // const endpoint = baseUrl + 'api/SampleData/WeatherForecasts';
    const endpoint = 'https://localhost:5001/api/purchasing-power';
    
    http.get<PurchasingPower[]>(endpoint).subscribe(result => {
      this.records = result;
    }, error => console.error(error));
  }
}

interface PurchasingPower {
  year: number;
  city: string;
  category: string;
  value: number;
}
