import { Component } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidationErrors, Validators } from '@angular/forms';

interface LoginData {
  name?: string | null ;
  numRue?: number | null;
  nomRue?: string | null;
  codePost?: string | null;
  commentaire?: string | null;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'reactive.form';

  loginForm : FormGroup<any> | undefined;
  loginData?: LoginData

  constructor(
    private fb: FormBuilder
  ) {
    this.loginForm = this.fb.group({
      name: ['',[Validators.required]],
      numRue: ['', [Validators.required,Validators.min(1000), Validators.max(9999)]],
      nomRue: ['', [Validators.required]],
      codePost: ['', ],
      commentaire:['', [Validators.required]],
    })
   }
}


