import { APP_BASE_HREF } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiserviceService } from '../apiservice.service';

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.css']
})
export class EmployeeComponent implements OnInit {

  //constructor() { }

  //ngOnInit() {
  //}
    EmployeeList: any = [];
    _entity: any;
    //public router: Router;
    constructor(private service: ApiserviceService,private router: Router
        //, public router: Router,
        //@Inject(APP_BASE_HREF) public baseHref: string
    ) {
        this.service.GetAllEmployees().subscribe(result => {
            this.EmployeeList = result;
            debugger
        }, error => console.error(error));

    }
    

    ngOnInit() {
       // this.refreshEmpList();
    }

    public onRowEditClick(entity) {


        this._entity = entity
        this.navigateToEditEmployee(entity)

        
            //const url = this.router.serializeUrl(
            //    this.router.createUrlTree([`${this.baseHref}/showEmployee/`, entity.Emp_ID])
            //);
            //window.open(url, '_blank');
       

    }
    navigateToEditEmployee(id: number) {
        //this.router.navigate(['employee/edit-Employee', id]);
        console.log(id);
        this.router.navigateByUrl('employee/edit-Employee', { state: { Emp_ID: id } });
        
    }

    //async refreshEmpList() {
        
    //    this.EmployeeList = await this.service.GetAllEmployees();
    //        //this.DepartmentListWithoutFilter = data;
        
    //}

}
