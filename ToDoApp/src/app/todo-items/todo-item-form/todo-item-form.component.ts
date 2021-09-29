import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { TodoItem } from 'src/app/models/todo-item.model';
import { TodoAppService } from 'src/app/services/todo-app.service';

@Component({
  selector: 'app-todo-item-form',
  templateUrl: './todo-item-form.component.html',
  styles: [
  ]
})
export class TodoItemFormComponent implements OnInit {

  constructor(public service: TodoAppService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    
  }

  onSubmit(form: NgForm) {
    console.log(this.service.todoData);
    if (this.service.todoData.itemId == 0) {
      console.log("Hello");
      this.insertRecord(form);
    } else {
      this.updateRecord(form);
    }

  }

  insertRecord(form: NgForm) {
    this.service.postToDoItem().subscribe(
      res => {
        this.resetForm(form);
        this.service.refreshList();
        this.toastr.success("Submitted successfully", "ToDo Item Register");
      },
      err => { console.log(err); }
    );
  }



  updateRecord(form: NgForm) {
    this.service.putToDoItem().subscribe(
      res => {
        this.resetForm(form);
        this.service.refreshList();
        this.toastr.info("Updated successfully", "ToDo Item Register");
      },
      err => { console.log(err); }
    );
  }

  resetForm(form: NgForm) {
    form.form.reset();
    this.service.todoData = new TodoItem();
  }

}
