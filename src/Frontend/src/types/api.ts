// API Response Types
export interface ApiResponse<T> {
  success: boolean;
  message?: string;
  data?: T;
  errors?: string[];
}

export interface PagedResponse<T> {
  items: T[];
  pageNumber: number;
  pageSize: number;
  totalCount: number;
  totalPages: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
}

// Property Types
export interface PropertyDto {
  id: string;
  title: string;
  price: number;
  bedrooms: number;
  bathrooms: number;
  city: string;
  thumbnailUrl: string;
  isFurnished: boolean;
  propertyType: string;
}

export interface PropertyDetailDto extends PropertyDto {
  description: string;
  squareFeet: number;
  address: string;
  state: string;
  zipCode: string;
  latitude: number;
  longitude: number;
  imageUrls: string[];
  isAvailable: boolean;
  agent?: AgentListDto;
  createdAt: string;
  updatedAt: string;
}

// Agent Types
export interface AgentListDto {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string;
  imageUrl?: string;
  propertyCount: number;
}

export interface AgentDetailDto extends AgentListDto {
  bio?: string;
  createdAt: string;
  properties: PropertyDto[];
}

// Inquiry Types
export interface PropertyInquiryDto {
  id: string;
  propertyId: string;
  contactName: string;
  email: string;
  phoneNumber: string;
  message: string;
  status: string;
  createdAt: string;
}

export interface CreatePropertyInquiryRequest {
  propertyId: string;
  contactName: string;
  email: string;
  phoneNumber: string;
  message: string;
}
