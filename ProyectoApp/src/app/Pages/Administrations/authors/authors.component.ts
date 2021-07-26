import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { AuthorModel } from '../../../Models/AuthorModel';
import { ResultModel } from '../../../Models/ResultModel';
import { SelectModel } from '../../../Models/SelectModel';
import { AuthorService } from '../../../Services/Author/author.service';
import { BookService } from '../../../Services/books/book.service';

@Component({
  selector: 'app-authors',
  templateUrl: './authors.component.html',
  styleUrls: ['./authors.component.css']
})
export class AuthorsComponent implements OnInit {

  constructor(private formBuilder: FormBuilder, private AuthorService: AuthorService, private BookService: BookService) { }

  form: FormGroup;
  Action = "Registro";

  ListBooks: SelectModel[];
  List: AuthorModel[] = [];

  showModal = false;


  ngOnInit(): void {

    this.BookService.SelectBooks().subscribe(
      ResultModel => {
        let Resu = ResultModel as ResultModel;
        if (!Resu.HasError) {
          console.log(Resu.Data);
          this.ListBooks = Resu.Data as SelectModel[];

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

    this.ListAllAuthors();

    this.form = this.formBuilder.group(
      {
        Id: '',
        IdBook: '',
        FirstName: '',
        LastName: '',
        ListBooks: '' 
 

      }
    );

  }

  SaveChanges() {
     
    if (this.Action == "Registro") {
      this.SaveAuthor();
    }

    if (this.Action == "Modificacion") {
      this.UpdateAuthor();
    }

  }

  SaveAuthor() {

    let Fields = this.GetFields();
    this.AuthorService.SaveAuthor(Fields).subscribe(
      ResultModel => {
        let Resu = ResultModel as ResultModel;
        if (!Resu.HasError) {
          alert(Resu.Messages);
          this.ListAllAuthors();
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

  ViewAuthor(id: number) {
    this.ShowModal(true, "Modificacion");
    this.GetAuthorByAuthorId(id);
  }

  UpdateAuthor() {

    let Fields = this.GetFields();

    this.AuthorService.UpdateAuthor(Fields).subscribe(
      ResultModel => {

        let Resu = ResultModel as ResultModel;
        if (!Resu.HasError) {

          alert(Resu.Messages);
          this.ListAllAuthors();
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

  ListAllAuthors() {

    this.AuthorService.GetAllAuthors().subscribe(

      ResultModel => {

        let Resu = ResultModel as ResultModel;

        if (!Resu.HasError) {

          let Array = Resu.Data as AuthorModel[];

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

  GetAuthorByAuthorId(id: number) {
    alert(id);
    this.AuthorService.GetAuthorByAuthorId(id).subscribe(

      ResultModel => {
        let Resu = ResultModel as ResultModel;

        if (!Resu.HasError) {
          let Author = Resu.Data as AuthorModel;
          this.SetFields(Author);
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


  //DeleteAuthor(id: number) {

  //  let respuesta = confirm("Esta seguro que desea eliminar el Authore?");

  //  if (respuesta)
  //    this.Service.DeleteAuthor(id).subscribe(
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
    this.form.controls['IdBook'].setValue("");
    this.form.controls['FirstName'].setValue("");
    this.form.controls['LastName'].setValue(""); 

  }

  GetFields() {

    let Field = new AuthorModel();

    Field.Id = this.form.get("Id").value;
    Field.IdBook = this.form.get("IdBook").value;
    Field.FirstName = this.form.get("FirstName").value;
    Field.LastName = this.form.get("LastName").value; 

    return Field;

  }

  SetFields(Author: AuthorModel) {

    this.form.controls['Id'].setValue(Author.Id);
    this.form.controls['IdBook'].setValue(Author.IdBook);
    this.form.controls['FirstName'].setValue(Author.FirstName);
    this.form.controls['LastName'].setValue(Author.LastName); 

  }
   
}  
