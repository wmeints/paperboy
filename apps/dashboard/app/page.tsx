import { GetServerSideProps } from 'next';
import { useState } from 'react';
import axios from 'axios';
import 'daisyui/dist/full.css';

interface Paper {
  id: string;
  title: string;
  submitter: {
    name: string;
    email: string;
  };
  url: string;
  status: string;
  sectionsSummarized: number;
  totalSections: number;
}

interface Props {
  papers: Paper[];
  currentPage: number;
  totalPages: number;
}

export const getServerSideProps: GetServerSideProps = async (context) => {
  const page = context.query.page ? parseInt(context.query.page as string, 10) : 0;
  const response = await axios.get(`http://localhost:3000/papers?page=${page}`);
  const { data } = response;

  return {
    props: {
      papers: data.items,
      currentPage: page,
      totalPages: Math.ceil(data.totalCount / 20),
    },
  };
};

const Page = ({ papers, currentPage, totalPages }: Props) => {
  const [currentPapers, setCurrentPapers] = useState(papers);

  const handleApprove = async (paperId: string) => {
    await axios.post(`http://localhost:3000/papers/${paperId}/approve`);
    setCurrentPapers(currentPapers.map(paper => paper.id === paperId ? { ...paper, status: 'approved' } : paper));
  };

  const handleDecline = async (paperId: string) => {
    await axios.post(`http://localhost:3000/papers/${paperId}/decline`);
    setCurrentPapers(currentPapers.map(paper => paper.id === paperId ? { ...paper, status: 'declined' } : paper));
  };

  return (
    <div className="container mx-auto p-4">
      <h1 className="text-2xl font-bold mb-4">Dashboard</h1>
      <button className="btn btn-primary mb-4">Submit New Paper</button>
      <div className="overflow-x-auto">
        <table className="table w-full">
          <thead>
            <tr>
              <th>Title</th>
              <th>Submitter</th>
              <th>URL</th>
              <th>Status</th>
              <th>Sections Summarized</th>
              <th>Total Sections</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            {currentPapers.map((paper) => (
              <tr key={paper.id}>
                <td>{paper.title}</td>
                <td>{paper.submitter.name}</td>
                <td><a href={paper.url} target="_blank" rel="noopener noreferrer">{paper.url}</a></td>
                <td><span className={`badge ${getStatusBadgeClass(paper.status)}`}>{paper.status}</span></td>
                <td>{paper.sectionsSummarized}</td>
                <td>{paper.totalSections}</td>
                <td>
                  {paper.status === 'scored' && (
                    <>
                      <button className="btn btn-success mr-2" onClick={() => handleApprove(paper.id)}>Approve</button>
                      <button className="btn btn-error" onClick={() => handleDecline(paper.id)}>Decline</button>
                    </>
                  )}
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
      <div className="flex justify-center mt-4">
        <button className="btn" disabled={currentPage === 0} onClick={() => handlePageChange(currentPage - 1)}>Previous</button>
        <span className="mx-2">Page {currentPage + 1} of {totalPages}</span>
        <button className="btn" disabled={currentPage === totalPages - 1} onClick={() => handlePageChange(currentPage + 1)}>Next</button>
      </div>
    </div>
  );

  function getStatusBadgeClass(status: string) {
    switch (status) {
      case 'imported':
        return 'badge-info';
      case 'summarized':
        return 'badge-warning';
      case 'scored':
        return 'badge-primary';
      case 'approved':
        return 'badge-success';
      case 'declined':
        return 'badge-error';
      default:
        return '';
    }
  }

  function handlePageChange(page: number) {
    window.location.href = `?page=${page}`;
  }
};

export default Page;
