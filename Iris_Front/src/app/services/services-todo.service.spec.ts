import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController,  } from '@angular/common/http/testing';
import { ServicesTodoService } from './services-todo.service';
import { ToDo } from '../models/ToDo';
import { Login } from '../models/login';

describe('ServicesTodoService', () => {
  let service: ServicesTodoService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [ServicesTodoService],
    });
    service = TestBed.inject(ServicesTodoService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should get auth token', () => {
    const mockResponse = { token: '12345' };
    const login: Login = { Username: 'test', Password: 'password' };

    service.GetAuthToken(login.Username, login.Password).subscribe((response) => {
      expect(response).toEqual(mockResponse);
    });

    const req = httpMock.expectOne(`${service.baseURL}/Auth/login`);
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual(login);
    req.flush(mockResponse);
  });

  it('should add a todo', () => {
    const mockResponse = true;
    const todo: ToDo = { description: 'test', isCompleted: false, isFavorite: false, id: '' };

    service.AddToDo(todo).subscribe((response) => {
      expect(response).toEqual(mockResponse);
    });

    const req = httpMock.expectOne(`${service.baseURL}/ToDo`);
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual(todo);
    req.flush(mockResponse);
  });

  it('should get todo list', () => {
    const mockResponse = { data: [] };
    const favorite = false;
    const completed = false;

    service.GetToDoList(favorite, completed);

    const req = httpMock.expectOne(`${service.baseURL}/ToDo`);
    expect(req.request.method).toBe('GET');
    req.flush(mockResponse);
  });

  it('should favorite a todo', () => {
    const mockResponse = true;
    const toDoId = "1";
    const isChecked = true;

    service.FavoriteToDo(toDoId, isChecked).subscribe((response) => {
      expect(response).toEqual(mockResponse);
    });

    const req = httpMock.expectOne(`${service.baseURL}/ToDo/${toDoId}/favorite?favorite=${isChecked}`);
    expect(req.request.method).toBe('PUT');
    req.flush(mockResponse);
  });

  it('should complete a todo', () => {
    const mockResponse = true;
    const toDoId = "1";
    const isChecked = true;

    service.CompleteToDo(toDoId, isChecked).subscribe((response) => {
      expect(response).toEqual(mockResponse);
    });

    const req = httpMock.expectOne(`${service.baseURL}/ToDo/${toDoId}/done?completed=${isChecked}`);
    expect(req.request.method).toBe('PUT');
    req.flush(mockResponse);
  });


});