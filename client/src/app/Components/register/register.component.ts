import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators, AbstractControl, ValidationErrors } from '@angular/forms';
import { AuthService } from '../../Services/auth.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent {
  registerForm: FormGroup;

  constructor(private fb: FormBuilder, private authService: AuthService) {
    this.registerForm = this.fb.group({
      name: ['', [Validators.required, this.noWhitespaceValidator]],
      email: ['', [Validators.required, Validators.email, this.noWhitespaceValidator]],
      password: ['', [Validators.required, this.noWhitespaceValidator]],
      address: ['', [Validators.required, this.noWhitespaceValidator]],
      contact: ['', [Validators.required, Validators.minLength(10), Validators.maxLength(10), this.noWhitespaceValidator]],
      pincode: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(6), this.noWhitespaceValidator]],
    });
  }

  onSubmit(): void {
    if (this.registerForm.valid) {
      const userData = this.registerForm.value;
      this.authService.register(userData).subscribe({
        next: (response) => {
          this.authService.setTokenAndNavigateToHome(response.token);
        },
      });
    }
  }

  // Custom validator to check for spaces
  noWhitespaceValidator(control: AbstractControl): ValidationErrors | null {
    const isWhitespace = (control.value || '').trim().length === 0;
    const isValid = !isWhitespace;
    return isValid ? null : { whitespace: true };
  }
}
