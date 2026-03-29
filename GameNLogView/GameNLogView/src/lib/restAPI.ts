export async function fetchGames(page: number = 1, search: string = ""){
  const params = new URLSearchParams();
  params.set("page", page.toString());
  if (search !== "")
    params.set("search", search);

  const games = await fetch(`http://localhost:8080/api/games?${params}`);
  const json = await games.json();
  return json;
}