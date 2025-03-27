import { Component, OnInit, Input } from '@angular/core';
import { ISimpleComboSelectionChangingEventArgs } from 'igniteui-angular';
import { ToDo } from './models/ToDo';
import { ServicesTodoService } from './services/services-todo.service';
import { ToastrService } from 'ngx-toastr';
import { Login } from './models/login';
import { Response } from './models/Response';
import { NavbarComponent } from "./components/navbar/navbar.component";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  standalone: false,

})
export class AppComponent implements OnInit {
  title = 'Iris_Front';
  titleToDo  ="";
  public lData : string[] = ['All','Favorite', 'Completed'];
  completed: boolean = false;
  favorite: boolean = false;
  @Input()
  todoInput!: ToDo;

  public login : Login = {Username : "usuario", Password : "1234"}
    public todoListType: string  = this.lData[0];
    constructor(public todoService: ServicesTodoService, private toasterService : ToastrService) { 
    }

    ngOnInit(): void {
      this.todoListType = this.lData[0];
      this.todoService.GetAuthToken("usuario","1234").subscribe((data: Response) => {
        console.log(data);
        localStorage.setItem('jwt', data.data);
      });
      this.getToDoList();
    }
    
    onSubmit(){
      if(this.titleToDo !== "")
      {
        let toDoAdd : ToDo = {description:this.titleToDo, isCompleted:false, isFavorite:false, id:""};
        this.todoService.AddToDo(toDoAdd).subscribe((respose: boolean) => {
          this.getToDoList();
          this.titleToDo = ""
        });
      }
      
      
    }
  
    selectChanging(e: ISimpleComboSelectionChangingEventArgs){
      
      this.todoListType = e.newSelection;
      
      if (e.newSelection === "Favorite")
      {
        this.favorite = true;
      }
      else if (e.newSelection === "Completed"){
        this.completed = true;
      }
      else{
        this.favorite = false;
        this.completed = false;
      }
      this.todoService.GetToDoList(this.favorite, this.completed);
    }

    getToDoList()
    {
      this.todoService.GetToDoList(false, false);
    }

    onChange(id : any, isChecked : boolean){
      this.todoService.CompleteToDo(id, !isChecked).subscribe((respose: boolean) => {
        this.getToDoList();
      });
    };

    onChangeFavorite(id : any, isChecked : boolean){
      this.todoService.FavoriteToDo(id, !isChecked).subscribe((respose: boolean) => {
        this.getToDoList();
      });
    };
    
    deleteTodo(id: any)
    {
    this.todoService.DeleteToDo(id).subscribe((respose: boolean) => {
      this.getToDoList();
    });
  }

  
}


