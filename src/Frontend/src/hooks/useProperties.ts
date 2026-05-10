import { useQuery } from '@tanstack/react-query';
import { apiClient } from '@services/apiClient';
import { AgentDetailDto, AgentListDto, PropertyDetailDto, PropertyDto, PagedResponse } from '@types/api';

export type PropertySearchParams = Record<string, string | number | boolean | undefined>;

export function useProperties(pageNumber: number = 1, pageSize: number = 20) {
  return useQuery({
    queryKey: ['properties', pageNumber, pageSize],
    queryFn: () => apiClient.getAllProperties(pageNumber, pageSize),
    select: (response) => response.data as PagedResponse<PropertyDto>,
  });
}

export function useSearchProperties(params: PropertySearchParams) {
  return useQuery({
    queryKey: ['properties-search', params],
    queryFn: () => apiClient.searchProperties(params),
    select: (response) => response.data as PagedResponse<PropertyDto>,
  });
}

export function useProperty(id?: string) {
  return useQuery({
    queryKey: ['property', id],
    queryFn: () => apiClient.getPropertyById(id as string),
    select: (response) => response.data as PropertyDetailDto,
    enabled: Boolean(id),
  });
}

export function useAgents() {
  return useQuery({
    queryKey: ['agents'],
    queryFn: () => apiClient.getAllAgents(),
    select: (response) => response.data as AgentListDto[],
  });
}

export function useAgent(id?: string) {
  return useQuery({
    queryKey: ['agent', id],
    queryFn: () => apiClient.getAgentById(id as string),
    select: (response) => response.data as AgentDetailDto,
    enabled: Boolean(id),
  });
}
