namespace Illallangi.WhatCD.Model.Artists
{
    public class Request
    {
        public int requestId { get; set; }
        public int categoryId { get; set; }
        public string title { get; set; }
        public int year { get; set; }
        public string timeAdded { get; set; }
        public int votes { get; set; }
        public long bounty { get; set; }
    }
}