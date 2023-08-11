import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiserviceService {
    readonly apiUrl = 'https://localhost:44366/api/Employee';

    constructor(private http: HttpClient) { }

    //GetAllEmployees(): Observable<any[]> {
    //    return this.http.get<any[]>(this.apiUrl + '/GetAllEmployees');
    //}
    //GetAllEmployees(): Promise<any[]> {
    //    return this.http.get<any[]>(this.apiUrl + '/GetAllEmployees').toPromise();
    //}

    GetAllEmployees() {
        var response = this.http.get(this.apiUrl + '/GetAllEmployees');
        return response;
    }
    GetEmpById(id: number) {
        
        var response = this.http.get(this.apiUrl + '/GetEmpDetailed/' + id);
        return response;
    }
}
