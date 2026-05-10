import axios, { AxiosInstance } from 'axios';
import {
  ApiResponse,
  PagedResponse,
  PropertyDto,
  PropertyDetailDto,
  AgentListDto,
  AgentDetailDto,
  CreatePropertyInquiryRequest,
  PropertyInquiryDto,
} from '@types/api';
import type { PropertySearchParams } from '@hooks/useProperties';

const API_URL = import.meta.env.VITE_API_URL || 'http://localhost:5000/api/v1';

class ApiClient {
  private client: AxiosInstance;

  constructor() {
    this.client = axios.create({
      baseURL: API_URL,
      headers: {
        'Content-Type': 'application/json',
      },
    });

    // Request interceptor
    this.client.interceptors.request.use(
      (config) => {
        return config;
      },
      (error) => Promise.reject(error),
    );

    // Response interceptor
    this.client.interceptors.response.use(
      (response) => response.data,
      (error) => {
        // Handle errors globally
        console.error('API Error:', error);
        return Promise.reject(error);
      },
    );
  }

  // Properties API
  async getAllProperties(
    pageNumber: number = 1,
    pageSize: number = 20,
  ): Promise<ApiResponse<PagedResponse<PropertyDto>>> {
    return this.client.get('/properties', {
      params: { pageNumber, pageSize },
    });
  }

  async getPropertyById(id: string): Promise<ApiResponse<PropertyDetailDto>> {
    return this.client.get(`/properties/${id}`);
  }

  async searchProperties(params: PropertySearchParams): Promise<ApiResponse<PagedResponse<PropertyDto>>> {
    return this.client.get('/properties/search', { params });
  }

  // Agents API
  async getAllAgents(): Promise<ApiResponse<AgentListDto[]>> {
    return this.client.get('/agents');
  }

  async getAgentById(id: string): Promise<ApiResponse<AgentDetailDto>> {
    return this.client.get(`/agents/${id}`);
  }

  // Inquiries API
  async createInquiry(
    request: CreatePropertyInquiryRequest,
  ): Promise<ApiResponse<PropertyInquiryDto>> {
    return this.client.post('/inquiries', request);
  }
}

export const apiClient = new ApiClient();
