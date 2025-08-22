import { useEffect, useState } from "react";
import { Container, Typography, List, ListItem } from "@mui/material";
import { api } from "./api";

type Cliente = { id: number; razonSocial: string };

export default function App() {
  const [clientes, setClientes] = useState<Cliente[]>([]);
  useEffect(() => {
    api.get<Cliente[]>("/clientes").then(r => setClientes(r.data));
  }, []);
  return (
    <Container sx={{ mt: 4 }}>
      <Typography variant="h4" gutterBottom>El Mudador (demo)</Typography>
      <Typography variant="h6">Clientes</Typography>
      <List>
        {clientes.map(c => <ListItem key={c.id}>{c.razonSocial}</ListItem>)}
      </List>
    </Container>
  );
}
