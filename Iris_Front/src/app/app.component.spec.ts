import { TestBed, ComponentFixture } from '@angular/core/testing';
import { AppComponent } from './app.component';
import { ServicesTodoService } from './services/services-todo.service';
import { ToastrService } from 'ngx-toastr';
import { NavbarComponent } from "./components/navbar/navbar.component";

import { of } from 'rxjs';
import { ToDo } from './models/ToDo';

describe('AppComponent', () => {
  let component: AppComponent;
  let navComponent: NavbarComponent;
  let fixture: ComponentFixture<AppComponent>;
  let fixtureNavbar: ComponentFixture<NavbarComponent>;
  let mockTodoService: jasmine.SpyObj<ServicesTodoService>;
  let mockToastrService: jasmine.SpyObj<ToastrService>;
  const mockTasks: ToDo[] = [
    {
      id: "1", description: 'Test Task 1', isCompleted: false,      isFavorite: false
    },
    {
      id: "2", description: 'Test Task 2', isCompleted: true, 
      isFavorite: false
    }
  ];

  beforeEach(async () => {
    mockTodoService = jasmine.createSpyObj('ServicesTodoService', [
      'GetAuthToken',
      'AddToDo',
      'GetToDoList',
      'CompleteToDo',
      'FavoriteToDo',
      'DeleteToDo',
    ]);

    mockToastrService = jasmine.createSpyObj('ToastrService', ['success', 'error', 'info', 'warning']);

    await TestBed.configureTestingModule({
      imports: [NavbarComponent], // Import both standalone components
      providers: [
        { provide: ServicesTodoService, useValue: mockTodoService },
        { provide: ToastrService, useValue: mockToastrService },
      ],
    }).compileComponents();

    // fixtureNavbar = TestBed.createComponent(NavbarComponent);
    // navComponent = fixtureNavbar.componentInstance;

    fixture = TestBed.createComponent(AppComponent);
    component = fixture.componentInstance;
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });
});