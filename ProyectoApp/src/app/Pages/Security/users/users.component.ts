import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { SelectModel } from '../../../Models/SelectModel';
import { UserModel } from '../../../Models/UserModel';
import { ResultModel } from '../../../Models/ResultModel';
import { UserService } from '../../../Services/User/user.service';  

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {

  constructor(private formBuilder: FormBuilder, private UserService: UserService) { }

  form: FormGroup;
  Action = "Registro";

  List: UserModel[] = []; 

  showModal = false; 

  ListRolsByUser: any;
  UserRol: any;
  RolsIds: any[] = [];

  ngOnInit(): void {
     

    this.ListAllUsers();

    this.form = this.formBuilder.group(
      {
        Id: '',

        UserName: '',
        Password: '' 

      }
    );

  }

  SaveChanges() {

    if (this.ValidateForm()) {
      return false;
    };

    if (this.Action == "Registro") {
      this.SaveUser();
    }

    if (this.Action == "Modificacion") {
      this.UpdateUser();
    }

  }

  SaveUser() {

    let Fields = this.GetFields(); 
    this.UserService.SaveUser(Fields).subscribe(
      ResultModel => {
        let Resu = ResultModel as ResultModel;
        if (!Resu.HasError) {
          alert(Resu.Messages);
          this.ListAllUsers();
          this.ShowModal(false, "Registro");
        } else {
          alert(Resu.Messages);
        }
      }, error => {

        if (error.status == 401) {
          alert("No Autorizado");
        } else {
          alert(JSON.stringify(error));
        }

      }
    );

  }

  ViewUser(id: number) {
    this.ShowModal(true, "Modificacion");
    this.GetUserByUserId(id);
  }

  UpdateUser() {

    let Fields = this.GetFields();

    this.UserService.UpdateUser(Fields).subscribe(
      ResultModel => {

        let Resu = ResultModel as ResultModel;
        if (!Resu.HasError) {

          alert(Resu.Messages);
          this.ListAllUsers();
          this.ShowModal(false, "Registro");

        } else {
          alert(Resu.Messages);
        }
      }, error => {

        if (error.status == 401) {
          alert("No Autorizado");
        } else {
          alert(JSON.stringify(error));
        }

      }
    );
  }

  ListAllUsers() {

    this.UserService.GetAllUsers().subscribe(

      ResultModel => {

        let Resu = ResultModel as ResultModel;

        if (!Resu.HasError) {

          let Array = Resu.Data as UserModel[];

          if (Resu.Data) {
            this.List = Array;
          } else {
            console.log('sin datos para mostrar')
          }

        } else {
          alert(Resu.Messages);
        }

      }, error => {

        if (error.status == 401) {
          alert("No Autorizado");
        } else {
          alert(JSON.stringify(error));
        }

      }
    );

  }

  GetUserByUserId(id: number) {
    alert(id);
    this.UserService.GetUserByUserId(id).subscribe(

      ResultModel => {
        let Resu = ResultModel as ResultModel;

        if (!Resu.HasError) {
          let User = Resu.Data as UserModel;
          this.SetFields(User);
        }

        console.log(Resu);

      }, error => {

        if (error.status == 401) {
          alert("No Autorizado");
        } else {
          alert(JSON.stringify(error));
        }

      }
    );

  }
    

  ShowModal(View: boolean, Action: string) {

    if (!View) {
      this.CleanFields();
    }
    this.showModal = View;
    this.Action = Action;
  }
   

  //DeleteUser(id: number) {

  //  let respuesta = confirm("Esta seguro que desea eliminar el Usere?");

  //  if (respuesta)
  //    this.Service.DeleteUser(id).subscribe(
  //      ResultModel => {
  //        let Resu = ResultModel as ResultModel;

  //        if (!Resu.HasError) {
  //          this.ListAllDependencies();
  //          alert(Resu.Messages);
  //        } else {
  //          alert(Resu.Messages);
  //        }

  //      }, error => {
  //        alert(JSON.stringify(error));
  //      }
  //    );
  //}
 

  CleanFields() {

    this.form.controls['Id'].setValue("");
    this.form.controls['UserName'].setValue("");
    this.form.controls['Password'].setValue("");  

  }

  GetFields() {

    let Field = new UserModel();

    Field.Id = this.form.get("Id").value;
    Field.UserName = this.form.get("UserName").value;
    Field.Password = this.form.get("Password").value; 

    return Field;

  }

  SetFields(User: UserModel) {

    this.form.controls['Id'].setValue(User.Id);
    this.form.controls['UserName'].setValue(User.UserName);
    this.form.controls['Password'].setValue(User.Password);

  }


  ValidateForm() {

    let HasError = false;
    let Data = this.GetFields();

    if (this.Action == "Registro") {
       
      if (Data.UserName === null || Data.UserName.length < 0) {
        alert("UserName es obligatorio");
        HasError = true;
      }
      if (Data.Password === null || Data.Password.length <= 2) {
        alert("Password es obligatorio");
        HasError = true;
      }
          
    }

    if (this.Action == "Modificacion") {

      if (Data.UserName === null || Data.UserName.length < 0) {
        alert("UserName es obligatorio");
        HasError = true;
      }
      if (Data.Password === null || Data.Password.length <= 2) {
        alert("Password es obligatorio");
        HasError = true;
      }

    }

    return HasError;
  }


}  
