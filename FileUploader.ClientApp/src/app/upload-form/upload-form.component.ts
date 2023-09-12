import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UploadModel } from '../models/upload-model';
import { UploadService } from '../services/upload.service';

@Component({
  selector: 'app-upload-form',
  templateUrl: './upload-form.component.html',
  styleUrls: ['./upload-form.component.css']
})
export class UploadFormComponent {
  postUploadForm: FormGroup;
  uploadData: UploadModel;
  selectedFile: File | null = null;

  fileExtensionErrorMessage: string = '';
  successMessage: string = '';

  isPostUploadFormSubmitted: boolean = false;

  constructor(private uploadService: UploadService) {
    this.postUploadForm = new FormGroup({
      email: new FormControl(null, [Validators.required, Validators.email]),
      file: new FormControl(null, [Validators.required])
    });
    this.uploadData = new UploadModel();
  }

  get postUploadForm_EmailControl(): any {
    return this.postUploadForm.controls['email'];
  }

  get postUploadForm_FileControl(): any {
    return this.postUploadForm.controls['file'];
  }

  onSelectFile(fileInput: any) {
    const file = fileInput.target.files[0];
    if (file != null) {
      const extension = file.name.split('.')[1].toLowerCase();
      if (extension !== 'docx') {
        this.fileExtensionErrorMessage = 'Invalid file extension! You should use files with .docx extenion';
        this.selectedFile = null;
        return;
      }
    }
    this.selectedFile = file;
    this.fileExtensionErrorMessage = '';
  }

  async clearSuccessMessage(delay: number) {
    setTimeout(() => {
      this.successMessage = '';
    }, delay)
  }

  public postUploadSubmitted() {
    this.isPostUploadFormSubmitted = true;

    if (this.postUploadForm.valid == false || !this.selectedFile) {
      return;
    }

    this.uploadData.email = this.postUploadForm_EmailControl.value;
    this.uploadData.file = this.selectedFile;
   
    this.uploadService.postUploadForm(this.uploadData).subscribe({
      next: (response: any) => {
        console.log(response);

        this.postUploadForm.reset();
        this.isPostUploadFormSubmitted = false;

        this.selectedFile = null;
        this.successMessage = 'File uploaded successfully!';
      },

      error: (error: any) => {
        console.log(error);
      },

      complete: () => {
        this.clearSuccessMessage(3500);
      }
    });

  }
}
