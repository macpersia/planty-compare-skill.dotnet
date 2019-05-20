import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public forecasts: WeatherForecast[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {

    // TODO: parameterize the base url
    // const endpoint = baseUrl + 'api/SampleData/WeatherForecasts';
    const endpoint = 'http://localhost:5000/' + 'api/SampleData/WeatherForecasts';
    
    http.get<WeatherForecast[]>(endpoint).subscribe(result => {
      this.forecasts = result;
    }, error => console.error(error));
  }
}

interface WeatherForecast {
  dateFormatted: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}
