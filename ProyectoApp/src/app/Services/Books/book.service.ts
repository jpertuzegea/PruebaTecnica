import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { BookModel } from '../../Models/BookModel';
import { ResultModel } from '../../Models/ResultModel';

@Injectable({
  providedIn: 'root'
})
export class BookService {

  constructor(private http: HttpClient) { }

  public GetBookByBookId(id: number) {
    return this.http.post(environment.BaseUrl + "api/Books/GetBookByBookId", id);
  }

  public GetAllBooks(): Observable<ResultModel> {
    return this.http.get<ResultModel>(environment.BaseUrl + "api/Books/BookList");
  }

  public SaveBook(Book: BookModel) {
    return this.http.post(environment.BaseUrl + "api/Books/BookAdd", Book);
  }

  public UpdateBook(Book: BookModel) {
    return this.http.put(environment.BaseUrl + "api/Books/BookUpdt", Book);
  }

  public SelectBooks(): Observable<ResultModel> {
    return this.http.get<ResultModel>(environment.BaseUrl + "api/Books/SelectBooks");
  }

  //public DeleteBook(id: number) {
  //  return this.http.post(environment.BaseUrl + "api/Books/DeleteClient", id);
  //}
}
