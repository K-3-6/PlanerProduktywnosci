export interface Task {
  id: number;
  title: string;
  description: string;
  category: 'Praca' | 'Dom' | 'Nauka' | 'Inne';
  priority: 'Niski' | 'Średni' | 'Wysoki';
  dueDate: string;
  status: 'Nowe' | 'W trakcie' | 'Wykonane';
}
