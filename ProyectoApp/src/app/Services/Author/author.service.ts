import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { AuthorModel } from '../../Models/AuthorModel';
import { ResultModel } from '../../Models/ResultModel';

@Injectable({
  providedIn: 'root'
})
export class AuthorService {

  constructor(private http: HttpClient) { }

  public GetAuthorByAuthorId(id: number) {
    return this.http.post(environment.BaseUrl + "api/Authors/GetAuthorByAuthorId", id);
  }

  public GetAllAuthors(): Observable<ResultModel> {
    return this.http.get<ResultModel>(environment.BaseUrl + "api/Authors/AuthorList");
  }

  public SaveAuthor(Author: AuthorModel) {
    return this.http.post(environment.BaseUrl + "api/Authors/AuthorAdd", Author);
  }

  public UpdateAuthor(Author: AuthorModel) {
    return this.http.put(environment.BaseUrl + "api/Authors/AuthorUpdt", Author);
  }


  //public DeleteAuthor(id: number) {
  //  return this.http.post(environment.BaseUrl + "api/Authors/DeleteClient", id);
  //}
}
