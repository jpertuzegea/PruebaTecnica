import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { BookModel } from '../../../Models/BookModel';
import { ResultModel } from '../../../Models/ResultModel'; 
import { BookService } from '../../../Services/books/book.service'; 

@Component({
  selector: 'app-books',
  templateUrl: './books.component.html',
  styleUrls: ['./books.component.css']
})
export class BooksComponent implements OnInit {

  constructor(private formBuilder: FormBuilder, private BookService: BookService) { }

  form: FormGroup;
  Action = "Registro";

  List: BookModel[] = [];
  showModal = false;
   

  ngOnInit(): void {
     
    this.ListAllBooks();

    this.form = this.formBuilder.group(
      {
        Id: '',
        Title: '',
        Description: '',
        PageCount: '',
        Excerpt: '',
        PublishDate: '' 
          
      }
    );

  }

  SaveChanges() {

    if (this.ValidateForm()) {
      return false;
    };

    if (this.Action == "Registro") {
      this.SaveBook();
    }

    if (this.Action == "Modificacion") {
      this.UpdateBook();
    }

  }

  SaveBook() {

    let Fields = this.GetFields();
    this.BookService.SaveBook(Fields).subscribe(
      ResultModel => {
        let Resu = ResultModel as ResultModel;
        if (!Resu.HasError) {
          alert(Resu.Messages);
          this.ListAllBooks();
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

  ViewBook(id: number) {
    this.ShowModal(true, "Modificacion");
    this.GetBookByBookId(id);
  }

  UpdateBook() {

    let Fields = this.GetFields();

    this.BookService.UpdateBook(Fields).subscribe(
      ResultModel => {

        let Resu = ResultModel as ResultModel;
        if (!Resu.HasError) {

          alert(Resu.Messages);
          this.ListAllBooks();
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

  ListAllBooks() {

    this.BookService.GetAllBooks().subscribe(

      ResultModel => {

        let Resu = ResultModel as ResultModel;

        if (!Resu.HasError) {

          let Array = Resu.Data as BookModel[];

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

  GetBookByBookId(id: number) {
    alert(id);
    this.BookService.GetBookByBookId(id).subscribe(

      ResultModel => {
        let Resu = ResultModel as ResultModel;

        if (!Resu.HasError) {
          let Book = Resu.Data as BookModel;
          this.SetFields(Book);
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


  //DeleteBook(id: number) {

  //  let respuesta = confirm("Esta seguro que desea eliminar el Booke?");

  //  if (respuesta)
  //    this.Service.DeleteBook(id).subscribe(
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
    this.form.controls['Title'].setValue("");
    this.form.controls['Description'].setValue("");
    this.form.controls['PageCount'].setValue("");
    this.form.controls['Excerpt'].setValue("");
    this.form.controls['PublishDate'].setValue(""); 
     
  }

  GetFields() {

    let Field = new BookModel();

    Field.Id = this.form.get("Id").value;
    Field.Title = this.form.get("Title").value;
    Field.Description = this.form.get("Description").value;
    Field.PageCount = this.form.get("PageCount").value;
    Field.Excerpt = this.form.get("Excerpt").value;
    Field.PublishDate = this.form.get("PublishDate").value; 

    return Field;

  }

  SetFields(Book: BookModel) {

    this.form.controls['Id'].setValue(Book.Id); 
    this.form.controls['Title'].setValue(Book.Title); 
    this.form.controls['Description'].setValue(Book.Description); 
    this.form.controls['PageCount'].setValue(Book.PageCount); 
    this.form.controls['Excerpt'].setValue(Book.Excerpt); 
    this.form.controls['PublishDate'].setValue(Book.PublishDate);  

  }


  ValidateForm() {

    let HasError = false;
    let Data = this.GetFields();

    if (this.Action == "Registro") {

      if (Data.Title === null || Data.Title.length < 0) {
        alert("Title es obligatorio");
        HasError = true;
      }
      if (Data.Description === null || Data.Description.length <= 2) {
        alert("Description es obligatorio");
        HasError = true;
      }

    }

    if (this.Action == "Modificacion") {
      if (Data.Title === null || Data.Title.length < 0) {
        alert("Title es obligatorio");
        HasError = true;
      }
      if (Data.Description === null || Data.Description.length <= 2) {
        alert("Description es obligatorio");
        HasError = true;
      }

    }

    return HasError;
  }


}  
