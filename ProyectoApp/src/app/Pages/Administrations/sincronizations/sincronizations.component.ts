import { Component, OnInit } from '@angular/core';
import { UserService } from '../../../Services/User/user.service';
import { ResultModel } from '../../../Models/ResultModel';
import { UserModel } from '../../../Models/UserModel';


@Component({
  selector: 'app-sincronizations',
  templateUrl: './sincronizations.component.html',
  styleUrls: ['./sincronizations.component.css']
})
export class SincronizationsComponent implements OnInit {

  List: UserModel[] = [];

  constructor(private UserService: UserService) { }

  ngOnInit(): void {
  }



  Sincronizar() {

    this.UserService.Sincronizate().subscribe(

      ResultModel => {

        let Resu = ResultModel as ResultModel;

        if (!Resu.HasError) {
          alert("Usuarios Sincronizados Con Exito");
          this.ListAllUsers();
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




}
