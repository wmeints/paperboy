import { useEffect, useState } from "react";

interface Paper {
  id: string;
  title: string;
  status: string;
  sectionsSummarized: number;
  totalSections: number;
  submitter: {
    name: string;
    email: string;
  };
}

export default function Page() {
  const [papers, setPapers] = useState<Paper[]>([]);
  const [page, setPage] = useState(0);
  const [totalPages, setTotalPages] = useState(0);

  useEffect(() => {
    fetch(`/papers?page=${page}`)
      .then((response) => response.json())
      .then((data) => {
        setPapers(data.results);
        setTotalPages(Math.ceil(data.totalCount / 20));
      });
  }, [page]);

  const handleApprove = (paperId: string) => {
    // Implement approve functionality
  };

  const handleDecline = (paperId: string) => {
    // Implement decline functionality
  };

  return (
    <div>
      <button onClick={() => {/* Implement submit paper functionality */}}>
        Submit another paper
      </button>
      <ul>
        {papers.map((paper) => (
          <li key={paper.id}>
            <h2>{paper.title}</h2>
            <p>Status: {paper.status}</p>
            <p>
              Sections Summarized: {paper.sectionsSummarized}/{paper.totalSections}
            </p>
            <p>Submitter: {paper.submitter.name} ({paper.submitter.email})</p>
            {paper.status === "scored" && (
              <div>
                <button onClick={() => handleApprove(paper.id)}>Approve</button>
                <button onClick={() => handleDecline(paper.id)}>Decline</button>
              </div>
            )}
          </li>
        ))}
      </ul>
      <div>
        {Array.from({ length: totalPages }, (_, index) => (
          <button key={index} onClick={() => setPage(index)}>
            {index + 1}
          </button>
        ))}
      </div>
    </div>
  );
}
