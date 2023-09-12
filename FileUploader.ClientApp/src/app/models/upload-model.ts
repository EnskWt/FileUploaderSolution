export class UploadModel {
  email: string | null;
  file: File | null;

  constructor(email: string | null = null, file: File | null = null) {
    this.email = email;
    this.file = file;
  }
}
