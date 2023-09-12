import { Injectable } from '@angular/core';
import { UploadModel } from '../models/upload-model';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

const API_BASE_URL = 'https://fileuploaderweb.azurewebsites.net/api/';
@Injectable({
  providedIn: 'root'
})
export class UploadService {

  constructor(private httpClient: HttpClient) { }

  public postUploadForm(uploadData: UploadModel): Observable<FormData> {
    const formData = new FormData();
    if (uploadData.email) {
      formData.append('email', uploadData.email);
    }

    if (uploadData.file) {
      formData.append('file', uploadData.file);
    }

    return this.httpClient.post<FormData>(`${API_BASE_URL}upload`, formData);
  }
}
