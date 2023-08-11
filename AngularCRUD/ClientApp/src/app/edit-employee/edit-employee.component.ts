import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ApiserviceService } from '../apiservice.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-edit-employee',
  templateUrl: './edit-employee.component.html',
  styleUrls: ['./edit-employee.component.css']
})
export class EditEmployeeComponent implements OnInit {

    form: FormGroup;
    Emp_id: any;
    Employee: any;
    //Emp_id: any;
    constructor(private service: ApiserviceService, private route: ActivatedRoute) {
        //this.service.GetAllEmployees().subscribe(result => {
        //    this.EmployeeList = result;
        //}, error => console.error(error));
        this.Emp_id = this.route
            .data
            .subscribe(v => console.log(v));
        debugger
        this.service.GetEmpById(this.Emp_id).subscribe(result => {
            this.Employee = result;
        }, error => console.error(error));

        //console.log(this.Employee.Emp_ID)
        //console.log(this.Employee.Emp_Name)
        //console.log(this.Employee.Emp_Gender)



        /*GetEmpById(id: number)*/
    }
    
    ngOnInit() {

        //console.log(this.Employee.Emp_ID)
        //console.log(this.Employee.Emp_Name)
        //console.log(this.Employee.Emp_Gender)
        //this.route.params.subscribe(params => {
        //    const id = +params['id']; // The plus sign converts the parameter to a number if it's a string
        //    // Now you can use the 'id' value in your component as needed
        //});

        
        //this.Emp_id = v;

        //this.form = new FormGroup({
        //    Employee_Name: new FormControl('', [Validators.required]),
        //    Employee_Gender: new FormControl('', Validators.required),
        //    Employee_Phone: new FormControl('', [Validators.required])
        //    //Employee_Gender: new FormControl('', Validators.required)
        //});
  }

}
