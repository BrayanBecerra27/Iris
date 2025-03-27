import { Injectable } from '@angular/core';
import { ToDo } from '../models/ToDo';
import { Login } from '../models/login';
import { Response } from '../models/Response';
// import { ToastrService } from 'ngx-toastr';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";
import { numberToString } from 'igniteui-angular-core';

@Injectable({
  providedIn: 'root'
})
export class ServicesTodoService {
  baseURL: string = "http://localhost:5286";

  constructor(private http: HttpClient) { }
  GetAuthToken(name: string, password: string): Observable<any> {
    const login: Login ={
      Username: name,
      Password: password
    };
    const request = this.http.post(
      `${this.baseURL}/Auth/login`,
      login
    );
    return request;
  }


  AddToDo(todo: ToDo): Observable<any> {
    const request = this.http.post<any>(`${this.baseURL}/ToDo`, todo, {
      headers: new HttpHeaders({
        Authorization: `Bearer ${localStorage.getItem('jwt')}`,
      }),
    });
    return request;
  }

  GetToDoList(favorite : boolean, completed : boolean)
  {
    const request = this.http.get<any>(
      `${this.baseURL}/ToDo`,
      {
        headers: new HttpHeaders({
          Authorization: `Bearer ${localStorage.getItem('jwt')}`,
        }),
      }).subscribe((response: any) => { 
        this.todoList = response.data;
        if (favorite)
        {
          this.todoList = this.todoList.filter(x => x.isFavorite === true);
        }
        if (completed) 
        {
          this.todoList = this.todoList.filter(x => x.isCompleted === true);
        }
      });
    
    }

  FavoriteToDo(toDoId: string,isChecked: boolean): Observable<boolean> {
    const request = this.http.put<boolean>(`${this.baseURL}/ToDo/`+ `${toDoId}` + `/favorite?favorite=` + `${isChecked}` ,"",  {
      headers: new HttpHeaders({
        Authorization: `Bearer ${localStorage.getItem('jwt')}`,
      }),
    });

    return request;
  }

  
  CompleteToDo(toDoId: string, isChecked: boolean): Observable<boolean> {
    const request = this.http.put<boolean>(`${this.baseURL}/ToDo/`+ `${toDoId}` + `/done?completed=` + `${isChecked}` ,"",  {
      headers: new HttpHeaders({
        Authorization: `Bearer ${localStorage.getItem('jwt')}`,
      }),
    });

    return request;
  }
  DeleteToDo(idTask: string): Observable<boolean> {
    const request = this.http.delete<boolean>(`${this.baseURL}/ToDo/`+ `${idTask}`,  {
      headers: new HttpHeaders({
        Authorization: `Bearer ${localStorage.getItem('jwt')}`,
      }),
    });

    return request;
  }
  todoList: ToDo[] = [];  
}


