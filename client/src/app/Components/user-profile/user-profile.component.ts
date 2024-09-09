import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../../Services/user.service.service';
import { UserProfileResponse } from '../../Models/user-profile-response.model';
import { CommonModule } from '@angular/common'; // Import CommonModule for *ngIf and other common directives
import { ReactiveFormsModule } from '@angular/forms'; // Import ReactiveFormsModule for forms

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css'],
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule] // Add the necessary imports here
})
export class UserProfileComponent implements OnInit {
  userForm!: FormGroup;
  isAdminUser: boolean = false;
  loading: boolean = false;
  errorMessage: string | null = null;
  successMessage: string | null = null; // Holds success message

  editingField: { [key: string]: boolean } = {
    name: false,
    address: false,
    contact: false,
    pinCode: false,
    email: false,
    password: false,
  };

  constructor(private userService: UserService, private fb: FormBuilder) {}

  ngOnInit(): void {
    this.userForm = this.fb.group({
      name: [{ value: '', disabled: true }, Validators.required],
      address: [{ value: '', disabled: true }],
      contact: [{ value: '', disabled: true }, Validators.required],
      pinCode: [{ value: '', disabled: true }],
      email: [{ value: '', disabled: true }, [Validators.required, Validators.email]],
      password: [{ value: '', disabled: true }, Validators.required],
      isAdmin: [{ value: false, disabled: true }]
    });

    this.getUserDetails();
  }

  getUserDetails(): void {
    this.loading = true;
    this.userService.getUserDetails().subscribe(
      (data: UserProfileResponse) => {
        this.userForm.patchValue({
          name: data.name,
          address: data.address,
          contact: data.contact,
          pinCode: data.pinCode,
          email: data.email,
          password: '',
          isAdmin: data.isAdmin
        });
        this.isAdminUser = data.isAdmin;
        this.loading = false;
      },
      (error: any) => { 
        console.error('Failed to load user details', error);
        
        this.errorMessage = 'Failed to load user details ${error}.';
        this.loading = false;
      }
    );
  }

  enableEditing(field: string): void {
    this.editingField[field] = true;
    this.userForm.get(field)?.enable();
  }

  disableEditing(field: string): void {
    this.editingField[field] = false;
    this.userForm.get(field)?.disable();
  }

  saveField(field: string): void {
    if (this.userForm.get(field)?.valid) {
      this.loading = true;
      const updatedField: Partial<UserProfileResponse> = { [field]: this.userForm.get(field)?.value }; // Create a partial object
  
      
      this.userService.updateUserDetails(updatedField).subscribe(
        (response: any) => {
          this.errorMessage = null; // Clear any previous error messages
          this.successMessage = `${field} updated successfully.`; // Set the success message
          this.loading = false;
          this.disableEditing(field); 
          setTimeout(() => {
            this.successMessage = null;
          }, 3000);
        
        },
        (error: any) => {
          console.error('Failed to change user details', error);
          console.log(error);
          this.errorMessage = `Failed to update ${field}.`;
          this.loading = false;
        }
      );
    } else {
      this.errorMessage = `Please fill out the ${field} field correctly.`;
    }
  }
}
