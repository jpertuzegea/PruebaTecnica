import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment'; 
import { ResultModel } from '../../Models/ResultModel'; 
import { UserModel } from '../../Models/UserModel';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  public GetUserByUserId(id: number) { 
    return this.http.post(environment.BaseUrl + "api/Users/GetUserByUserId", id);
  }
  
  public GetAllUsers(): Observable<ResultModel> {
    return this.http.get<ResultModel>(environment.BaseUrl + "api/Users/UserList");
  }
   
  public SaveUser(User: UserModel) {
    return this.http.post(environment.BaseUrl + "api/Users/UserAdd", User);
  }

  public UpdateUser(User: UserModel) {
    return this.http.put(environment.BaseUrl + "api/Users/UserUpdt", User);
  }

  public Sincronizate(): Observable<ResultModel> {
    return this.http.get<ResultModel>(environment.BaseUrl + "api/Users/Sincronizate");
  }
   
  //public DeleteUser(id: number) {
  //  return this.http.post(environment.BaseUrl + "api/Users/DeleteClient", id);
  //}

}

