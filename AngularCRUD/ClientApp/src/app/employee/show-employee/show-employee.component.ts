import { Component, OnInit } from '@angular/core';
import { ApiserviceService } from '../../apiservice.service';

@Component({
  selector: 'app-show-employee',
  templateUrl: './show-employee.component.html',
  styleUrls: ['./show-employee.component.css']
})
export class ShowEmployeeComponent implements OnInit {

    constructor(private service: ApiserviceService) { }
    EmployeeList: any = [];
    ngOnInit() {
        this.refreshEmpList();
  }

    refreshEmpList() {
        //this.service.GetAllEmployees().subscribe(data => {
        //    this.EmployeeList = data;
        //    //this.DepartmentListWithoutFilter = data;
        //});
    }
}
