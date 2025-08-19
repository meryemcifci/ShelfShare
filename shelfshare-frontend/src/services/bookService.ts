import api from '@/lib/api'

export type BookDto = {
  id: number
  title: string
  author: string
}

export async function getBooks() {
  const { data } = await api.get<BookDto[]>('/books') // API endpoint
  return data
}
